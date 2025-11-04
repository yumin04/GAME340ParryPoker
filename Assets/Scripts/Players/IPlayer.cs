using SOFile;
using UnityEngine;


public abstract class IPlayer : MonoBehaviour
{
    protected UserDataSO playerData;
    public void AddCard(CardDataSO cardData)
    {
        playerData.cards.Add(cardData);
    }

    public void AddCard(CardObject cardObject)
    {
        playerData.cards.Add(cardObject.GetCardData());
    }
    
    public void KeepCard(CardDataSO cardData)
    {
        AddCard(cardData);
    }
    public void KeepCard(CardObject cardData)
    {
        AddCard(cardData);
    }
    // Notify(this)
}
