
using SOFile;
using UnityEngine;

public class Match : MonoBehaviour
{
    private static Match instance;

    private MatchDataSO matchData;

    public static Match GetInstance() => instance;

    
    [SerializeField] private GameObject oneGameObject;
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
        // Getting Game Data SO for the round
        matchData = Resources.Load<MatchDataSO>("InGameData/MatchDataSO");
        StartGame();
    }

    public void StartGame()
    {
        Instantiate(oneGameObject);
        // TODO: Refactor all names in Game Instance
        Game game = Game.GetInstance();
        game.LoadGameData();
        game.ResetGameData();
        game.AddAllCardsForGame();
        game.InstantiateTableObject();
        game.InitializeGame();
        
    }

    public void GameEnd(IPlayer player, int damage)
    {
        if (player is Player)
        {
            Debug.Log("Player wins the game!");
            matchData.opponentHealth -= damage;
        }
        else if (player is Computer)
        {
            Debug.Log("Computer wins the game!");
            matchData.playerHealth -= damage;
        }
        else
        {
            Debug.LogWarning("Unknown player type in GameEnd.");
        }
    }
}
