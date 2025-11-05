using System;
using UnityEngine;
using UnityEngine.UI;

public class KeepButtonListener : IButtonListener
{
    protected override void Awake()
    {
        base.Awake(); // 반드시 base 호출!
    }
    public override void OnClick()
    {
        //PlayerClicked
        // TODO: Refactor
        // Game.GetInstance().Notify("Player Keep Button Clicked");
        Debug.Log("OnClickKeepButtonListener");
        Action action = OneRound.GetInstance().GetOnPlayerChoseKeep();
        action.Invoke();
    }
}