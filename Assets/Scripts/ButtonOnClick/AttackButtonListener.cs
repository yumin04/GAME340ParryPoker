using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonListener : IButtonListener
{    
    protected override void Awake()
    {
        base.Awake();
    }
    public override void OnClick()
    {
        //PlayerClicked
        // TODO: Refactor
        // Game.GetInstance().Notify("Player Attack Button Clicked");
        Action action = OneRound.GetInstance().GetOnPlayerChoseAttack();
        action.Invoke();
    }
}