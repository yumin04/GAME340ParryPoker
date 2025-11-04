using System.Collections.Generic;
using SOFile;
using UnityEngine;
using UnityEngine.UI;


public class Player : IPlayer
{
    
    [SerializeField] private Transform cardDisplayParent; // Panel
    [SerializeField] private GameObject cardDisplayPrefab; // Card UI prefab

    private static Player instance;

    public static Player GetInstance()
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
        playerData = Resources.Load<UserDataSO>("InGameData/PlayerHandSO");
        
        if (playerData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }

        playerData.ResetData();
    }
    
    public void DisplayPlayerCard()
    {
        foreach (Transform child in cardDisplayParent)
            Destroy(child.gameObject);

        foreach (var card in playerData.cards)
        {
            var cardObj = Instantiate(cardDisplayPrefab, cardDisplayParent);
            var display = cardObj.AddComponent<PlayerCardDisplay>();
            display.AddCardData(card);
            display.FlipCardFrontwards();
        }
    }

}