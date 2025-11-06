using System;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonListener : IButtonListener
{
    // TODO: Make this a physical object so use OnMouseDown with BoxColider2D
    public override void OnClick()
    {
        //PlayerClicked
        Debug.Log("[DEBUG] Before OnclickCardOnTable Invoke");
        GameEvents.OnClickCardOnTable.Invoke(Player.GetInstance());
        Debug.Log("[DEBUG] After OnclickCardOnTable Invoke");
    }
}