using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonListener : IAttackOrKeepButtonListener
{    
    public override void OnClick()
    {
        //PlayerClicked
        Action action = OneRound.GetInstance().GetOnPlayerChoseAttack();
        Table.GetInstance().SetIsPlayer();
        GameEvents.OnAttackOrKeepButtonClicked.Invoke(this);
        action.Invoke();
    }
}