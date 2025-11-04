using System.Collections.Generic;
using UnityEngine;



namespace SOFile
{
    [CreateAssetMenu(fileName = "TableData", menuName = "ScriptableObjects/TableDataSO")]
    public class TableDataSO : ScriptableObject
    {
        [Header("hand")]
        public List<CardDataSO> cards;

        public Sprite cardBackImage;

        [Header("match data")] 
        public int roundRemaining;
        public int maxRound = 10;
        public int playerHealth = 100;
        public int opponentHealth = 100;
        public int cardVisibleDuration = 15;


        
        public void ResetDataForRound()
        {
            cards.Clear();
            roundRemaining = maxRound;
            Debug.Log("["+this.name + "] Reset complete");
            
        }
    }
    
}