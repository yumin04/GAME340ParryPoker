using System;
using System.Collections;
using System.Collections.Generic;
using SOFile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Table : MonoBehaviour
{
    // private List<CardDataSO> playerCards;
    // private GameDataSO TableData;

    [SerializeField]private GameObject tableMainDisplayPrefab;
    private RectTransform tableMainDisplayParent; // Main Panel
    
    [SerializeField] private RectTransform tableTopDisplayParent; // Top Panel

    [SerializeField] private GameObject cardDisplayPrefab; // Card UI prefab

    [SerializeField] private GameObject cardObjectPrefab;

    [SerializeField] private TextMeshProUGUI cardCountdownText;
    
    private readonly WaitForSeconds oneSecond = new WaitForSeconds(1f);
    
    private static Table instance;
    
    
    
    private CardDataSO currentCardData;
    private CardDisplay currentCardDisplay;
    private bool isComputer;

    public void SetIsComputer()
    {
        isComputer = true;
    }

    public void SetIsPlayer()
    {
        isComputer = false;
    }
    public static Table GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        tableMainDisplayParent =  tableMainDisplayPrefab.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GameEvents.OnEndOfRound += OnRoundEnd;

        GameEvents.OnUserHavePriority += ShowChosenCard;
        
        GameEvents.OnAllAttackEnd += OnAllAttackEnd;
        GameEvents.OnComputerAttackEnd += OnAllAttackEnd;
    }

    private void OnDisable()
    {
        GameEvents.OnEndOfRound -= OnRoundEnd;
        
        GameEvents.OnUserHavePriority -= ShowChosenCard;
        
        GameEvents.OnAllAttackEnd -= OnAllAttackEnd;
        GameEvents.OnComputerAttackEnd -= OnAllAttackEnd;
    }


    private void OnRoundEnd(CardDataSO obj)
    {
        RemoveAllChildInMainTable();

    }

    private void RemoveAllChildInMainTable()
    {
        for (int i = tableMainDisplayParent.childCount - 1; i >= 0; i--)
        {
            Destroy(tableMainDisplayParent.GetChild(i).gameObject);
        }
    }

    public void StartFlipAllCardsCountdown(int cardVisibleDuration)
    {
        StartCoroutine(FlipAllCardsCountdown(cardVisibleDuration));
    }
    private IEnumerator FlipAllCardsCountdown(int cardVisibleDuration)
    {
        for (int i = cardVisibleDuration; i > 0; i--)
        {
            cardCountdownText.text = "Remaining Seconds: " + i;
            Debug.Log("Time Remaining: " + i);
            yield return oneSecond;
        }
        cardCountdownText.text = "";
        Action action = Game.GetInstance().GetOnTableCardShowEnd();
        Game.GetInstance().Notify(action);
    }

    
    public void DisplayTableCardsInMain(List<CardDataSO> cardsData)
    {
        foreach (Transform child in tableMainDisplayParent)
            Destroy(child.gameObject);

        foreach (var card in cardsData)
        {
            var cardObj = Instantiate(cardDisplayPrefab, tableMainDisplayParent);
            var display = cardObj.AddComponent<TableCardDisplay>();
            // TODO: Refactor
            display.AddCardData(card);
            
            // maybe use Invoke at the end?
            display.FlipCardFrontwards();
        }
    }

    public void ChangeTableDisplay()
    {
        for (int i = tableMainDisplayParent.childCount - 1; i >= 0; i--)
        {
            Transform child = tableMainDisplayParent.GetChild(i);
            child.SetParent(tableTopDisplayParent, false);
        }
    }



    // public void Reset()
    // {
    //     ResetTableDisplay();
    // }
    //
    // private void ResetTableDisplay()
    // {
    //     throw new System.NotImplementedException();
    // }
    public void ChooseOneCardFromTable()
    {
        if (tableTopDisplayParent.childCount == 0)
            return;

        // 1️⃣ 랜덤 자식 선택
        int randomIndex = Random.Range(0, tableTopDisplayParent.childCount);
        Transform chosenTransform = tableTopDisplayParent.GetChild(randomIndex);
        GameObject chosenCard = chosenTransform.gameObject;

        // 2️⃣ CardDisplay 가져오기
        currentCardDisplay = chosenCard.GetComponent<CardDisplay>();
        if (currentCardDisplay == null)
        {
            Debug.LogError("CardDisplay component missing on chosen card!");
            return;
        }

        // ✅ 현재 카드 데이터 설정
        currentCardData = currentCardDisplay.GetCardData();

        // 3️⃣ 클릭 리스너 추가 (없을 때만)
        if (chosenCard.GetComponent<CardButtonListener>() == null)
            chosenCard.AddComponent<CardButtonListener>();

        // 4️⃣ 부모를 main panel로 변경
        chosenTransform.SetParent(tableMainDisplayParent, false);

        // 5️⃣ 중앙 위치로 이동 및 확대
        chosenTransform.localPosition = Vector3.zero;
        chosenTransform.localScale = Vector3.one * 1.2f; // 살짝 확대 (선택 연출용)

        Debug.Log($"[ChooseOneCardFromTable] Selected card: {currentCardData.name}");
    }
    

    public CardDataSO ChooseOneCardFromTableAndGetCurrentRoundCard()
    {
        ChooseOneCardFromTable();
        return GetCurrentRoundCard();
    }

    public CardDataSO GetCurrentRoundCard()
    {
        return currentCardData;
    }

    public void ShowChosenCard()
    {
        currentCardDisplay.FlipCardFrontwards();
    }

    private void OnAllAttackEnd(int combo)
    {
        InitializeCardAttack(combo);
    }

    private void OnAllDefenceEnd()
    {
        
    }
    
    private void InitializeCardAttack(int combo)
    {
        GameObject obj = Instantiate(cardObjectPrefab);
        var ao = obj.GetComponent<AttackObject>();
        ao.SetCardData(currentCardData);
        ao.transform.position = Attack.GetInstance().GetAttackPosition();
        
        Debug.Log("[DEBUG] Current Combo Number = " + combo);
        switch (combo)
        {
            case 5:
                ao.AddModifier(new ColorIncreaseRedModifier(0.2f));
                ao.AddModifier(new SpeedModifier(1.5f));
                goto case 4;

            case 4:
                ao.AddModifier(new ColorIncreaseRedModifier(0.2f));
                ao.AddModifier(new SpeedModifier(1.5f));
                goto case 3;

            case 3:
                ao.AddModifier(new ColorIncreaseRedModifier(0.2f));
                ao.AddModifier(new SpeedModifier(1.0f));
                goto case 2;

            case 2:
                ao.AddModifier(new ColorIncreaseRedModifier(0.2f));
                ao.AddModifier(new RotationModifier(180f));
                goto case 1;

            case 1:
                ao.AddModifier(new ColorIncreaseRedModifier(0.2f));
                ao.AddModifier(new SpeedModifier(1f));
                break;
        }


        if (isComputer)
        {
            Debug.Log("Inside -1");
            ao.AddModifier(new DirectionModifier(-1f));
        }
        ao.ChangeBehavior(new MovingAttackBehavior());


    }


}