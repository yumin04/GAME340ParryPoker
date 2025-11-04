using System;
using System.Collections;
using SOFile;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game instance;

    public static Game GetInstance()
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

    void Start()
    {
        StartCoroutine(InitializeGame());
    }
    

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
        
        //Table initialization
        Table.GetInstance().InitializeTable();
    }


    // TODO: REFACTOR
    // TODO: REFACTOR
    // TODO: VERY IMPORTANT REFACTOR
    // TODO: WILL TAKE LONG
    public void Notify(string argument)
    {
        if (argument == "Game Started")
        {
            StartGame();
        } 
        else if (argument == "Player Prio")
        {
            // Computer.GetInstance().StopCatchCoroutine();
            HandlePlayerPrio();
            // Let player decide
            // Attack or Keep
            // Card Front will be shown in the middle for player
            // Initialize Panel in UI with 
                // Attack Button
                // Keep Button
        }
        else if (argument == "Computer Prio")
        {
            HandleComputerPrio();
            // Let Computer Decide
            // Attack or Keep
            // Card Back will be shown to the player
            // This will be random 50 - 50 chance
        }
        else if (argument == "Player Attack Pressed")
        {
            
        }
        else if (argument == "Player Keep Pressed")
        {
            
        }
        else if (argument == "Card Initialized To Table")
        {
            // Computer.Getinstance.TryCatchCoroutine();
        }
        else if (argument == "Next Round")
        {
            
        }
        else if (argument == "End Game")
        {
            
        }
        else if (argument == "End Match")
        {
            
        }
    }

    public Action GetHandleComputerPrio() => HandleComputerPrio;
    public Action GetHandlePlayerPrio() => HandlePlayerPrio;
    private void HandleComputerPrio()
    {
        // GameDataSO cardData = Table.GetInstance().GetCurrentCardData();
        // Computer.GetInstance().ChooseAttackOrKeep(cardData);
        throw new NotImplementedException();
    }

    private void HandlePlayerPrio()
    {
        // UI.GetInstance().DisplayUserSelectActionPanel();
        throw new NotImplementedException();
    }

    private void HandlePlayerChoseAttack()
    {
        //Computer.GetInstance().Keep(cardData);
        // Table.GetInstance().AttackChosen();
    }

    private void HandlePlayerChoseKeep()
    {
        // GameDataSO cardData = Table.GetInstance().GetCurrentCardData();
        // Player.GetInstance().Keep(cardData);
    }
    private void HandleComputerChoseAttack()
    {
        // GameDataSO cardData = Table.GetInstance().GetCurrentCardData();
        // Player.GetInstance().Keep(cardData);
        // Table.GetInstance().AttackChosen();
    }

    private void HandleComputerChoseKeep()
    {
        // GameDataSO cardData = Table.GetInstance().GetCurrentCardData();
        // Computer.GetInstance().Keep(cardData);
    }

    // TODO: Something like IPlayer needs to be used for proper refactoring
    // public void HandleAttackPressed(IPlayer player)
    // {
    //     throw new NotImplementedException();
    // }
    
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
    private void StartGame()
    {
        // Flip card and put board on top
        FlipTableCards();
        PutTableOnTop();
        
        // Start Round Countdown
        StartCoroutine(StartRoundCountdown());
        // Table에서 한개의 card를 select해서
        // 중간에 init
    }

    public IEnumerator StartRoundCountdown()
    {
        yield return new WaitForSeconds(2f);
        Table.GetInstance().ChooseOneCardFromTable();
    }
    // TODO: Animate
    private void FlipTableCards() => GameEvents.OnFlipCountdownEnd?.Invoke();
    private void PutTableOnTop()=> Table.GetInstance().ChangeTableDisplay();
}