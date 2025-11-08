using System;
using System.Collections;
using SOFile;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance;
    
    private GameDataSO gameTableData;

    [SerializeField] private GameObject roundGameObject;
    [SerializeField] private GameObject tableGameObject;
    

    public static Game GetInstance() => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnNewGameStarted += ResetGameTableData;
        GameEvents.OnEndOfRound += OnRoundEnd;
    }

    private void OnDisable()
    {
        GameEvents.OnNewGameStarted -= ResetGameTableData;
        GameEvents.OnEndOfRound -= OnRoundEnd;
    }

    void Start()
    {
        // Getting Game Data SO for the round
        gameTableData = Resources.Load<GameDataSO>("InGameData/GameDataSO");
        
        if (gameTableData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }
        
        // Reset Data
        GameEvents.OnNewGameStarted.Invoke();

        
        
        // Add all the round cards here
        AddAllCardsForGame();
        
        // Initialize Table Object
        Instantiate(tableGameObject);

        // Initialize Game for players and display player card
        StartCoroutine(InitializeGame());

        // Display All Cards 
        Table.GetInstance().DisplayTableCardsInMain(gameTableData.cards);
        Table.GetInstance().StartFlipAllCardsCountdown(gameTableData.cardVisibleDuration);

    }
    
    private void AddAllCardsForGame()
    {
        // 1️⃣ 기존 카드 초기화
        gameTableData.cards.Clear();

        // 2️⃣ 원하는 개수만큼 랜덤 카드 추가 (ex: 10장)
        int cardCount = gameTableData.maxRound;
        var assigner = CardAssigner.GetInstance();

        for (int i = 0; i < cardCount; i++)
        {
            CardDataSO randomCard = assigner.GetRandomCard();
            gameTableData.cards.Add(randomCard);
        }

        Debug.Log($"[{nameof(AddAllCardsForGame)}] Added {cardCount} random cards to gameTableData.");
    }

    private void ResetGameTableData() => gameTableData.ResetDataForGame();
    
    private IEnumerator InitializeGame()
    {
        yield return null; // 한 프레임 기다려서 모든 Start() 실행 이후 실행
        // Player와 Computer에게 각각 2장씩
        for (int i = 0; i < 2; i++)
        {
            CardDataSO playerCard = CardAssigner.GetInstance().GetRandomCard();
            Player.GetInstance().AddCard(playerCard);
            CardDataSO computerCard = CardAssigner.GetInstance().GetRandomCard();
            Computer.GetInstance().AddCard(computerCard);
        }
        Player.GetInstance().DisplayPlayerCard();
    }

    
    public Action GetOnTableCardShowEnd()
    {
        return OnTableCardShowEnd;
    }

    private void OnTableCardShowEnd() => StartGame();
    private void StartGame()
    {
        // Flip card and put board on top
        FlipTableCards();
        PutTableOnTop();
        
        // Initialize OneRoundGameObject
        OnNewRound();
    }

    // public Action GetOnRoundEnd() => OnRoundEnd;

    // TODO: implement observer here
    private void OnRoundEnd(CardDataSO cardData)
    {
        if (gameTableData.cards.Remove(cardData))
        {
            Debug.Log("[Game.cs] Removed Successfully");
        }
        else
        {
            Debug.Log("[Game.cs] Not removed. Make sure to fix this properly");
        }
        
        // Observer used so we do not need to do this
        if (gameTableData.roundRemaining > 0)
        {
            OnNewRound();
        }
        else
        {
            CalculateWinnerOfGame();
        }
    }

    private void CalculateWinnerOfGame()
    {
        int playerDamage = Player.GetInstance().CalculateHand();
        int computerDamage = Computer.GetInstance().CalculateHand();
        
        throw new NotImplementedException();
    }

    private void OnNewRound()
    {
        gameTableData.roundRemaining--; 
        Instantiate(roundGameObject);
    }


    public void Notify(Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public void Notify(Action<Player> action)
    {
        if (action != null)
        {
            action.Invoke(Player.GetInstance());
        }
    }
    
    
    public void Notify(Action<Computer> action)
    {
        if (action != null)
        { 
            action.Invoke(Computer.GetInstance());
        }
    }

    

    
    // TODO: Animate
    private void FlipTableCards() => GameEvents.OnFlipCountdownEnd?.Invoke();
    private void PutTableOnTop()=> Table.GetInstance().ChangeTableDisplay();
}