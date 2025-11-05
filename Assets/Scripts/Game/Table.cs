using System;
using System.Collections;
using System.Collections.Generic;
using SOFile;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Table : MonoBehaviour
{
    // private List<CardDataSO> playerCards;
    // private GameDataSO TableData;


    [SerializeField] private RectTransform tableMainDisplayParent; // Main Panel
    [SerializeField] private RectTransform tableTopDisplayParent; // Top Panel

    [SerializeField] private GameObject cardDisplayPrefab; // Card UI prefab
    
    private readonly WaitForSeconds oneSecond = new WaitForSeconds(1f);
    
    private static Table instance;

    private CardDataSO currentCardData;
    private CardDisplay currentCardDisplay;
    
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
    }

    public void StartFlipAllCardsCountdown(int cardVisibleDuration)
    {
        StartCoroutine(FlipAllCardsCountdown(cardVisibleDuration));
    }
    private IEnumerator FlipAllCardsCountdown(int cardVisibleDuration)
    {
        for (int i = 0; i < cardVisibleDuration; i++)
        {
            Debug.Log("Time Remaining: " + i);
            yield return oneSecond;
        }

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
}