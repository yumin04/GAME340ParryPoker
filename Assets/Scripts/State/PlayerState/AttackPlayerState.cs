using UnityEngine;

public class AttackPlayerState : IPlayerState
{
    public override void HandleSpaceKey()
    {
        Debug.Log("Currently AttackPlayerState");
        GameEvents.OnAttackRoundEnd.Invoke();
    }
}