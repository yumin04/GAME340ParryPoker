using System;
using UnityEngine;
using UnityEngine.UI;


public abstract class ISelectionButton : IButtonListener
{
    protected Image image;
    
    protected Color normalColor = Color.white;
    protected Color selectedColor = new Color(0.7f, 0.7f, 1f);

    protected bool isSelected = false;

    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        if (image == null)
            image = gameObject.AddComponent<Image>();
    }
    private void Select()
    {
        isSelected = true;
        image.color = selectedColor;
    }

    private void Deselect()
    {
        isSelected = false;
        image.color = normalColor;
        DisableButtonWhenClicked();
    }
    
    public void OnOtherButtonClicked(IButtonListener clicked)
    {
        if (clicked == this)
            Select();
        else
            Deselect();
    }
}