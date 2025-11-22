
using System;
using System.Collections;
using System.Collections.Generic;
using SOFile;
using UnityEngine;
using Random = UnityEngine.Random;

public class Computer : IPlayer
{

    private static Computer instance;

    public static Computer GetInstance() => instance;
    
    private Coroutine catchCoroutine;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        playerHandData = Resources.Load<UserDataSO>("InGameData/OpponentHandSO");

        if (playerHandData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }
    }
    
    
    protected override void OnEnable()
    {
        GameEvents.OnUserHavePriority += StopCatchCoroutine;
        GameEvents.OnPlayerAttackChosen += InitializeDefendObject;
        GameEvents.OnComputerAttackChosen += InitializeAttackObject;
        
        base.OnEnable(); 
    }
    protected override void OnDisable()
    {
        GameEvents.OnUserHavePriority -= StopCatchCoroutine;
        GameEvents.OnPlayerAttackChosen -= InitializeDefendObject;
        GameEvents.OnComputerAttackChosen -= InitializeAttackObject;
        base.OnDisable(); 
    }

    protected override void InitializeDefendObject()
    {
        base.InitializeDefendObject();
        GameEvents.OnSetDefencePlayer.Invoke(Computer.GetInstance());
        DefendAttack();
    }

    protected override void InitializeAttackObject()
    {
        attackDefenceInitializer.InstantiateAttackObject();
        AttackOpponent();
    }
    public void StartCatchCoroutine() => catchCoroutine = StartCoroutine(CatchCoroutine());
    public void StopCatchCoroutine() => StopCoroutine(catchCoroutine);
    
    private IEnumerator CatchCoroutine()
    {
        float randomDelay = Random.Range(1f, 2f);
        yield return new WaitForSeconds(randomDelay);
        
        // Computer has clicked the card
        GameEvents.OnClickCardOnTable.Invoke(Computer.GetInstance());
        
        Debug.Log($"[CatchCoroutine] Invoked computer priority after {randomDelay:F2} seconds");
    }
    
    private PlayerChoice RandomChoice()
    {
        if (Random.value < 0.5f)
        {
            Debug.Log("Computer Chose Attack");
            return PlayerChoice.Attack;
        }
        Debug.Log("Computer Chose Keep");
        return PlayerChoice.Keep;
    }

    public void ChooseAttackOrKeep()
    {
        // Random selection between 2 choices
        // TODO: Refactor this with game events as well
        Action action;
        if (RandomChoice() == PlayerChoice.Attack)
        {
            action = OneRound.GetInstance().GetOnComputerChoseAttack();
            action.Invoke();
        }
        else
        {
            action = OneRound.GetInstance().GetOnComputerChoseKeep();
            action.Invoke();
        }
    }

    private void AttackOpponent()
    {
        // 1️⃣ 랜덤으로 콤보 개수 정하기
        int combo = Random.Range(1, 4); // 예: 1~3 콤보
        Attack.GetInstance().SetCombo(combo);
        Table.GetInstance().SetIsComputer();
        Attack.GetInstance().EndAttackLoop();
    }

    private void DefendAttack()
    {
        bool canDefend = Random.value > 0.5f;

        if (canDefend)
        {
            Debug.Log("Will Be Defending");
            GameEvents.OnDefendInput?.Invoke();
        }
        else
        {
            Debug.Log("Defense failed!");
        }
    }

    public override void HavePriority()
    {
        ChooseAttackOrKeep();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Computer Taking Damage");
        // TODO: Refactor
        Action action;
        action = OneRound.GetInstance().GetOnComputerChoseKeep();
        action.Invoke();
    }
}
