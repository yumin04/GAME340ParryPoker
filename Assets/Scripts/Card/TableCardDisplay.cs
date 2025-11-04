using SOFile;
using UnityEngine;
using UnityEngine.UI;

public class TableCardDisplay : CardDisplay
{
    private CardDataSO cardData;
    private Image image;
    private void OnEnable()
    {
        GameEvents.OnFlipCountdownEnd += FlipCardBackwards;
    }

    private void OnDisable()
    {
        GameEvents.OnFlipCountdownEnd -= FlipCardBackwards;
    }
    
}