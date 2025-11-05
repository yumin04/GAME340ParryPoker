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
        playerHandData = Resources.Load<UserDataSO>("InGameData/PlayerHandSO");
        if (playerHandData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }
        DontDestroyOnLoad(gameObject);
    }
    // public void Start()
    // {
    //
    //     
    //
    //
    //     playerHandData.ResetData();
    // }
    
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

}