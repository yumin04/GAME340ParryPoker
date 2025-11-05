using System;
using System.Collections;
using SOFile;
using UnityEngine;

public class OneRound : MonoBehaviour
{
    private static OneRound instance;

    public static OneRound GetInstance() => instance;

    private CardDataSO currentRoundCardData;
    public CardDataSO GetCurrentRoundCardData() => currentRoundCardData;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Start()
    {
        StartCoroutine(StartRoundCountdown());
    }
    
    public IEnumerator StartRoundCountdown()
    {
        // TODO: make sure time is random here
        yield return new WaitForSeconds(2f);
        currentRoundCardData = Table.GetInstance().ChooseOneCardFromTableAndGetCurrentRoundCard();
        Computer.GetInstance().StartCatchCoroutine();
    }
    
    
    // METHOD GETTERS
    public Action GetOnComputerPrio() => OnComputerPrio;
    public Action GetOnPlayerPrio() => OnPlayerPrio;
    public Action GetOnPlayerChoseAttack() => OnPlayerChoseAttack;
    public Action GetOnPlayerChoseKeep() => OnPlayerChoseKeep;
    public Action GetOnComputerChoseAttack() => OnComputerChoseAttack;
    public Action GetOnComputerChoseKeep() => OnComputerChoseKeep;

    
    // METHODS
    private void OnPlayerChoseAttack()
    {
        Computer.GetInstance().KeepCard(currentRoundCardData);
        // TODO: Implement Attack
    }
    
    // TODO: Discuss if Keep is needed
    private void OnPlayerChoseKeep()
    {
        Player.GetInstance().KeepCard(currentRoundCardData);
    }
    private void OnComputerChoseAttack()
    {
        Player.GetInstance().KeepCard(currentRoundCardData);
        // TODO: Implement Attack
    }
    
    // TODO: Discuss if Keep is needed
    private void OnComputerChoseKeep()
    {
        Computer.GetInstance().KeepCard(currentRoundCardData);
    }
    
    private void OnComputerPrio()
    {
        Computer.GetInstance().ChooseAttackOrKeep();
    }
    private void OnPlayerPrio()
    {
        GameUI.GetInstance().EnableUserSelectActionPanel();
        
        Table.GetInstance().ShowChosenCard();
        
        Computer.GetInstance().StopCatchCoroutine();
    }
    

    public void Notify(Action action)
    {
        action.Invoke();
    }
}
