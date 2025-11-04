using SOFile;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardDisplay : CardDisplay
{
    private CardDataSO cardData;
    private Image image;
    
    private void OnEnable()
    {
        // GameEvents.OnFlipCountdownEnd += FlipCardBackwards;
    }

    private void OnDisable()
    {
        // GameEvents.OnFlipCountdownEnd -= FlipCardBackwards;
    }
}