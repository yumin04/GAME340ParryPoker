using System;
using System.Collections.Generic;
using SOFile;
using UnityEngine;

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

    
    public int CalculateHand()
    {
        // TODO: Think about this: 
        // What if I create a struct called "hand data"
        List<CardDataSO> hand = playerHandData.cards;
        List<HandRank> handModifier = new List<HandRank>();
                
        // Check Flush
        // Check Straight
            // Check Royal
            // Check Back
        // if both, check straight flush

        // Check Quads, Triple, and Pair
        
        // Check Full House
        
        throw new NotImplementedException();
    }

}

// TODO: Create New Hand Rank when duplicate cards are available
public enum HandRank
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    BackStraight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalStraightFlush
}