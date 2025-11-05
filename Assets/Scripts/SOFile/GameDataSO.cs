using System.Collections.Generic;
using UnityEngine;


// TODO: Decide if this is in Table or Game
namespace SOFile
{
    [CreateAssetMenu(fileName = "TableData", menuName = "ScriptableObjects/GameDataSO")]
    public class GameDataSO : ScriptableObject
    {
        [Header("hand")]
        public List<CardDataSO> cards;

        public Sprite cardBackImage;

        [Header("match data")] 
        public int roundRemaining;
        public int maxRound = 10;

        // TODO: Refactor so this goes into Match Data
        // or player data
        public int playerHealth = 100;
        public int opponentHealth = 100;
        public int cardVisibleDuration = 15;


        
        public void ResetDataForGame()
        {
            cards.Clear();
            roundRemaining = maxRound;
            Debug.Log("["+this.name + "] Reset complete");
            
        }
    }
    
}