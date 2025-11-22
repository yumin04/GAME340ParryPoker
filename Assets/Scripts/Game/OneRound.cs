using System;
using System.Collections;
using SOFile;
using UnityEngine;
using Random = UnityEngine.Random;

public class OneRound : MonoBehaviour
{
    
    // TODO: Refactor with observer pattern here
    // Not a full Singleton
    private static OneRound instance;

    public static OneRound GetInstance() => instance;

    
    [SerializeField] private GameObject userSelectActionPanel;
    
    private void EnableUserSelectActionPanel() => userSelectActionPanel.SetActive(true);

    private void DisableUserSelectActionPanel() => userSelectActionPanel.SetActive(false);

    
    [SerializeField] private CardDataSO currentRoundCardData;
    public CardDataSO GetCurrentRoundCardData() => currentRoundCardData;
    private void Awake()
    {
        // if (instance != null && instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        instance = this;

    }

    public void Start()
    {
        StartCoroutine(StartRoundCountdown());
    }
    private void OnEnable()
    {
        GameEvents.OnUserHavePriority += EnableUserSelectActionPanel;
        GameEvents.OnClickCardOnTable += OnCardPriorityDecided;
    }
    private void OnDisable()
    {
        GameEvents.OnUserHavePriority -= EnableUserSelectActionPanel;
        GameEvents.OnClickCardOnTable -= OnCardPriorityDecided;
    }
    
    public IEnumerator StartRoundCountdown()
    {
        float randomDelay = Random.Range(0.5f, 5f);
        yield return new WaitForSeconds(randomDelay);

        currentRoundCardData = Table.GetInstance().ChooseOneCardFromTableAndGetCurrentRoundCard();
        Computer.GetInstance().StartCatchCoroutine();
    }

    


    
    // METHOD GETTERS
    // public Action GetOnComputerPrio() => OnComputerPrio;
    public Action GetOnPlayerChoseAttack() => OnPlayerChoseAttack;
    public Action GetOnPlayerChoseKeep() => OnPlayerChoseKeep;
    public Action GetOnComputerChoseAttack() => OnComputerChoseAttack;
    public Action GetOnComputerChoseKeep() => OnComputerChoseKeep;

    // METHODS
    private void OnPlayerChoseAttack()
    {
        GameEvents.OnPlayerAttackChosen.Invoke();
    }
    
    private void OnPlayerChoseKeep()
    {
        Player.GetInstance().KeepCard(currentRoundCardData);
        EndRound();
    }

    public void OnKeep(IPlayer player)
    {
        player.KeepCard(currentRoundCardData);
        EndRound();
    }
    private void OnComputerChoseAttack()
    {
           
        GameEvents.OnComputerAttackChosen.Invoke();
    }
    
    private void OnComputerChoseKeep()
    {
        Computer.GetInstance().KeepCard(currentRoundCardData);
        EndRound();
    }
    
    private void OnComputerPrio()
    {
        Computer.GetInstance().ChooseAttackOrKeep();
    }


    // private void OnPlayerPrio()
    // {
    //
    //     
    //     Table.GetInstance().ShowChosenCard();
    //     
    //     Computer.GetInstance().StopCatchCoroutine();
    // }

    private void OnCardPriorityDecided(IPlayer player)
    {
        player.HavePriority();
    }
    
    public void Notify(Action action)
    {
        action.Invoke();
    }
    private void EndRound()
    {
        Destroy(gameObject);
        GameEvents.OnEndOfRound.Invoke(currentRoundCardData); 
    }
}
