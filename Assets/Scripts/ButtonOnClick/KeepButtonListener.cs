using System;
using UnityEngine;
using UnityEngine.UI;

public class KeepButtonListener : IAttackOrKeepButtonListener
{
    public override void OnClick()
    {
        Action action = OneRound.GetInstance().GetOnPlayerChoseKeep();
        GameEvents.OnAttackOrKeepButtonClicked.Invoke(this);
        action.Invoke();
    }
}