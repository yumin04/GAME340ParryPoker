using SOFile;
using UnityEngine;
using UnityEngine.UI;
public abstract class CardDisplay : MonoBehaviour
{
        private CardDataSO cardData;
        public CardDataSO GetCardData() => cardData;

        private Image image;
    
        public void Awake()
        {
            image = GetComponent<Image>();
        }
    
        public void FlipCardBackwards()
        {
            image.sprite = cardData.GetCardBackImage();
        }
    
        public void FlipCardFrontwards()
        {
            image.sprite = cardData.cardImage;
    
        }
    
        public void AddCardData(CardDataSO card)
        {
            cardData = card;
        }


}