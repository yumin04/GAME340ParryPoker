
using UnityEngine;

public class NullPlayerState : IPlayerState
{
    public override void HandleSpaceKey()
    {
        Debug.Log("Currently NullPlayerState");
        // Do nothing for now.
    }
}
