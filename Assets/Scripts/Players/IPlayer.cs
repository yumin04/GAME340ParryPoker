using SOFile;
using UnityEngine;

public enum PlayerChoice
{
    Attack,
    Keep
}

public abstract class IPlayer : MonoBehaviour
{
    protected UserDataSO playerHandData;
    private void OnEnable()
    {
        GameEvents.OnNewGameStarted += ResetPlayerHandData;   
    }

    private void OnDisable()
    {
        GameEvents.OnNewGameStarted -= ResetPlayerHandData;
    }

    public void AddCard(CardDataSO cardData) => playerHandData.cards.Add(cardData);


    public void AddCard(CardObject cardObject) => playerHandData.cards.Add(cardObject.GetCardData());

    public void KeepCard(CardDataSO cardData) => AddCard(cardData);
    
    public void KeepCard(CardObject cardData) => AddCard(cardData);
    
    
    public void ResetPlayerHandData() => playerHandData.ResetData();

    // Notify(this)
    public abstract void HavePriority();
}
