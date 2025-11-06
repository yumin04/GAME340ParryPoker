
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
        DontDestroyOnLoad(gameObject);
    }
    
    
    private void OnEnable()
    {
        GameEvents.OnUserHavePriority += StopCatchCoroutine;
    }
    private void OnDisable()
    {
        GameEvents.OnUserHavePriority -= StopCatchCoroutine;
    }


    public void StartCatchCoroutine() => catchCoroutine = StartCoroutine(CatchCoroutine());
    public void StopCatchCoroutine() => StopCoroutine(catchCoroutine);
    
    private IEnumerator CatchCoroutine()
    {
        // 1️⃣ 1~2초 랜덤 대기
        // TODO: change time
        float randomDelay = Random.Range(10f, 20f);
        yield return new WaitForSeconds(randomDelay);

        // 2️⃣ 컴퓨터 우선권 처리 호출
        // Computer has clicked the card
        GameEvents.OnClickCardOnTable.Invoke(Computer.GetInstance());
        
        Debug.Log($"[CatchCoroutine] Invoked computer priority after {randomDelay:F2} seconds");
    }
    
    private PlayerChoice RandomChoice()
    {
        if (Random.value < 0.5f)
        {
            return PlayerChoice.Attack;
        }
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

    public override void HavePriority()
    {
        ChooseAttackOrKeep();
    }
}
