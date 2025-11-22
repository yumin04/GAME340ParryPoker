using System;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonListener : IButtonListener
{
    public void OnEnable()
    {
        GameEvents.OnClickCardOnTable += DisableButtonWhenClicked;
    }

    public void OnDisable()
    {
        GameEvents.OnClickCardOnTable -= DisableButtonWhenClicked;
    }
    private void DisableButtonWhenClicked(IPlayer obj)
    {
        DisableButtonWhenClicked();
    }

    // TODO: Make this a physical object so use OnMouseDown with BoxColider2D
    public override void OnClick()
    {
        //PlayerClicked
        GameEvents.OnClickCardOnTable.Invoke(Player.GetInstance());
    }
}