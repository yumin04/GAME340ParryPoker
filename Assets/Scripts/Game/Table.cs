using System.Collections;
using System.Collections.Generic;
using SOFile;
using UnityEngine;
using UnityEngine.UI;


public class Table : MonoBehaviour
{
    // private List<CardDataSO> playerCards;
    private TableDataSO TableData;


    [SerializeField] private RectTransform tableMainDisplayParent; // Main Panel
    [SerializeField] private RectTransform tableTopDisplayParent; // Top Panel

    [SerializeField] private GameObject cardDisplayPrefab; // Card UI prefab
    
    private readonly WaitForSeconds oneSecond = new WaitForSeconds(1f);
    
    private static Table instance;
    
    
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
        DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        TableData = Resources.Load<TableDataSO>("InGameData/TableDataSO");
        
        if (TableData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }
    }

    public void InitializeTable()
    {
        // Add cards to table
        TableData.ResetDataForRound();
        for (int i = 0; i < TableData.maxRound; i++)
        {
            CardDataSO randomCard = CardAssigner.GetInstance().GetRandomCard();
            AddCard(randomCard);
        }
        // Display Table Cards for 15 seconds
        DisplayTableCard();
        StartCoroutine(FlipAllCardsCountdown());
    }

    private IEnumerator FlipAllCardsCountdown()
    {
        for (int i = 0; i < TableData.cardVisibleDuration; i++)
        {
            Debug.Log("Time Remaining: " + i);
            yield return oneSecond;
        }
        Game.GetInstance().Notify("Game Started");
        // SceneLoader.GetInstance().LoadRoundScene();
    }
    public void AddCard(CardDataSO cardData)
    {
        TableData.cards.Add(cardData);
    }

    public void AddCard(CardObject cardObject)
    {
        TableData.cards.Add(cardObject.GetCardData());
    }

    public void DisplayTableCard()
    {

        foreach (Transform child in tableMainDisplayParent)
            Destroy(child.gameObject);

        foreach (var card in TableData.cards)
        {
            var cardObj = Instantiate(cardDisplayPrefab, tableMainDisplayParent);
            var display = cardObj.AddComponent<TableCardDisplay>();
            display.AddCardData(card);
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
        Transform chosenCard = tableTopDisplayParent.GetChild(randomIndex);
 
        // 2️⃣ 부모를 main panel로 변경
        chosenCard.SetParent(tableMainDisplayParent, false);

        // 3️⃣ 중앙 위치로 이동 및 확대
        chosenCard.localPosition = Vector3.zero;
        
        // Notify Game that card has been initialized
    }


}