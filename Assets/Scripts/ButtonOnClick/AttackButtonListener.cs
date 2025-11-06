using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonListener : IButtonListener
{    
    public override void OnClick()
    {
        //PlayerClicked
        Action action = OneRound.GetInstance().GetOnPlayerChoseAttack();
        action.Invoke();
    }
}