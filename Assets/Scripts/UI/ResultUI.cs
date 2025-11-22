
using SOFile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    private static ResultUI instance;

    private MatchDataSO matchData;
    
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject computer;

    public static ResultUI GetInstance() => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        FindWinner();
    }

    private void FindWinner()
    {
        int winner = UnityEngine.Random.Range(0, 2);
        Debug.Log(winner);
        if (winner == 0)
        {
            resultText.text = "You Win!";
            player.SetActive(true);
            computer.SetActive(false);
        }
        else
        {
            resultText.text = "You Lost!";
            computer.SetActive(true);
            player.SetActive(false);
        }
    }

}
