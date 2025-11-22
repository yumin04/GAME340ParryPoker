using UnityEngine;

public class DefendPlayerState : IPlayerState
{
    
    public override void HandleSpaceKey()
    {
        Debug.Log("Currently DefendPlayerState");
        GameEvents.OnDefendInput.Invoke();
        
    }
}