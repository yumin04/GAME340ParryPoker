using System;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonListener : IButtonListener
{
    // TODO: Make this a physical object so use OnMouseDown with BoxColider2D
    public override void OnClick()
    {
        //PlayerClicked
        GameEvents.OnClickCardOnTable.Invoke(Player.GetInstance());
    }
}