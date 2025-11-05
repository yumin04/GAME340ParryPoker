using System;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonListener : IButtonListener
{
    protected override void Awake()
    {
        base.Awake(); // 반드시 base 호출!
    }
    public override void OnClick()
    {
        //PlayerClicked
        Action playerPrioAction = OneRound.GetInstance().GetOnPlayerPrio();
        OneRound.GetInstance().Notify(playerPrioAction);
    }
}