using UnityEngine;
public enum Suit
{
    Spade,
    Club,
    Heart,
    Diamond
}

namespace SOFile
{
    [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardDataSO")]
    public class CardDataSO : ScriptableObject
    {
        [Header("Card Info")]
        public Suit cardSymbol;
        public int cardNumber;
        public Sprite cardImage;
        public Sprite cardBackImage;

        public Sprite GetCardBackImage()
        {
            if (cardBackImage == null)
            {
                cardBackImage = Resources
                    .Load<BackfaceImage>("CardData/CardBackImage/BackfaceImage")
                    .cardBackImage;
            }
            return cardBackImage;
        }
    }
    
}