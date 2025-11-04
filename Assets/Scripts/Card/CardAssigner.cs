using UnityEngine;
using System.Collections.Generic;
using SOFile;

public class CardAssigner : MonoBehaviour
{
    private static CardAssigner instance;
    
    public GameObject cardPrefab;
    private GameObject cardPrefabInstance;


    public static CardAssigner GetInstance()
    {
        return instance;
    }

    private List<CardDataSO> allCardData = new List<CardDataSO>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        LoadAllCardData();
    }


    public void LoadAllCardData()
    {
        allCardData.Clear();
        
        string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };

        foreach (string suit in suits)
        {
            CardDataSO[] loaded = Resources.LoadAll<CardDataSO>($"CardData/{suit}");
            allCardData.AddRange(loaded);
        }
    }

    public CardDataSO[] GetCards(int numCards)
    {
        CardDataSO[] allCards = new CardDataSO[numCards];
        for (int i = 0; i < numCards; i++)
        {
            allCards[i] = GetRandomCard();
        }
        return allCards;
    }
    public CardDataSO GetRandomCard()
    {
        if (allCardData.Count == 0)
        {
            Debug.LogWarning("[CardAssigner] No card data loaded!");
            return null;
        }

        int randomIndex = Random.Range(0, allCardData.Count);
        CardDataSO randomCard = allCardData[randomIndex];
        
        return randomCard;
    }

    public void OnClickInstantiateNewCard()
    {
        // 기존 카드 삭제
        if (cardPrefabInstance != null)
        {
            Destroy(cardPrefabInstance);
        }

        // 새 카드 생성
        cardPrefabInstance = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);

        // 랜덤 카드 데이터 선택
        if (allCardData.Count > 0)
        {
            int randomIndex = Random.Range(0, allCardData.Count);
            CardDataSO randomCard = allCardData[randomIndex];

            // CardObject 컴포넌트에 데이터 붙이기
            CardObject cardObj = cardPrefabInstance.GetComponent<CardObject>();
            if (cardObj != null)
            {
                cardObj.AttachCardData(randomCard);
            }
            Player.GetInstance().AddCard(randomCard);
            Player.GetInstance().DisplayPlayerCard();
        }
        else
        {
            Debug.LogWarning("[CardAssigner] No card data loaded!");
        }
    }
}