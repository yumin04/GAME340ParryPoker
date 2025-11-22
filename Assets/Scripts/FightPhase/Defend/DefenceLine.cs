
using System;
using UnityEngine;

public class DefenceLine : DestroyOnRoundEnd
{
    private IPlayer _player;
    public void SetPlayer(IPlayer player) => this._player = player;
    protected override void OnEnable()
    {
        GameEvents.OnDefendInput += AddColliderComponent;
        GameEvents.OnSetDefencePlayer += SetPlayer;
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        GameEvents.OnDefendInput -= AddColliderComponent;
        GameEvents.OnSetDefencePlayer -= SetPlayer;
        base.OnDisable();
    }
    
    public void AddColliderComponent()
    {
        if (GetComponent<Collider2D>() != null)
            return;

        gameObject.AddComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // GameEvents.OnDefence;
        // Not taking damage
        // Action action;
        // action = OneRound.GetInstance().GetOnComputerChoseKeep();
        // action.Invoke();
        OneRound.GetInstance().OnKeep(_player);
    }
}
