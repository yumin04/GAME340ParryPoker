using System;
using System.Collections;
using System.Collections.Generic;
using SOFile;
using UnityEngine;
using UnityEngine.UI;


public class Player : IPlayer
{
    [SerializeField] private Transform cardDisplayParent; // Panel
    [SerializeField] private GameObject cardDisplayPrefab; // Card UI prefab

    private static Player instance;

    private IPlayerState playerState;
    private Coroutine disableMainActionCoroutine;

    public static Player GetInstance()
    {
        return instance;
    }

    protected override void OnEnable()
    {
        GameEvents.OnPlayerAttackChosen += InitializeAttackObject;
        GameEvents.OnPlayerAttackChosen += SetAttackState;

        GameEvents.OnComputerAttackChosen += SetDefendState;
        GameEvents.OnComputerAttackChosen += InitializeDefendObject;
        GameEvents.OnAllAttackEnd += OnAllAttackEnd;
        GameEvents.OnDefendInput += ChangeBackToNullState;
        base.OnEnable(); 
    }


    protected override void OnDisable()
    {
        GameEvents.OnPlayerAttackChosen -= InitializeAttackObject;
        GameEvents.OnPlayerAttackChosen -= SetAttackState;
        
        GameEvents.OnComputerAttackChosen -= SetDefendState;
        GameEvents.OnComputerAttackChosen -= InitializeDefendObject;
        
        GameEvents.OnAllAttackEnd -= OnAllAttackEnd;
        GameEvents.OnDefendInput -= ChangeBackToNullState;
        base.OnDisable(); 
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        playerHandData = Resources.Load<UserDataSO>("InGameData/PlayerHandSO");
        if (playerHandData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }

        // Initializing as Null for now
        playerState = new NullPlayerState();
    }


    public void DisplayPlayerCard()
    {
        foreach (Transform child in cardDisplayParent)
            Destroy(child.gameObject);

        foreach (var card in playerHandData.cards)
        {
            var cardObj = Instantiate(cardDisplayPrefab, cardDisplayParent);
            var display = cardObj.AddComponent<PlayerCardDisplay>();
            display.AddCardData(card);
            display.FlipCardFrontwards();
        }
    }

    public override void HavePriority()
    {
        GameEvents.OnUserHavePriority.Invoke();
    }
    

    public void HandleMainAction()
    {
        playerState.HandleSpaceKey();
    }

    private void SetAttackState() => playerState = new AttackPlayerState();

    private void SetDefendState()
    {
        playerState = new DefendPlayerState();
        Debug.Log("Setting to Defend State");
    }

    public void OnBadDefence() => disableMainActionCoroutine = StartCoroutine(FiveSecondNullState());

    private IEnumerator FiveSecondNullState()
    {
        SetNullState();
        yield return new WaitForSeconds(5f);
        SetDefendState();
    }

    private void ChangeBackToNullState()
    {
        if(disableMainActionCoroutine != null)
            StopCoroutine(disableMainActionCoroutine);
        SetNullState();
    }

    private void SetNullState() => playerState = new NullPlayerState();

    private void OnAllAttackEnd(int combo)
    {
        ChangeBackToNullState();
        // InitializeCardAttack(combo);
    }
    protected override void InitializeDefendObject()
    {
        base.InitializeDefendObject();
        GameEvents.OnSetDefencePlayer.Invoke(Player.GetInstance());
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Taking Damage");
        // TODO: Refactor
        Action action;
        action = OneRound.GetInstance().GetOnPlayerChoseKeep();
        action.Invoke();
    }
}