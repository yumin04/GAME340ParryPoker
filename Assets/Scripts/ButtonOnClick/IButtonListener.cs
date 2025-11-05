using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class IButtonListener : MonoBehaviour
{
    private Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
            button = gameObject.AddComponent<Button>();
        
        button.onClick.AddListener(OnClick);
    }

    public abstract void OnClick();
}