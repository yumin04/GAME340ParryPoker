using System;
using UnityEngine;
using UnityEngine.UI;

public class KeepButtonListener : IButtonListener
{
    public override void OnClick()
    {
        Action action = OneRound.GetInstance().GetOnPlayerChoseKeep();
        action.Invoke();
    }
}