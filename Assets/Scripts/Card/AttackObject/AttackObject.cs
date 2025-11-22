using UnityEngine;
using System.Collections.Generic;
using SOFile;

public class AttackObject : DestroyOnRoundEnd
{
    private List<IModifier> modifiers = new List<IModifier>();

    public float speed = 5f;
    public float rotationSpeed = 0f;   // degrees per second
    public float rotationAngle = 0f;   // 누적될 회전값
    public Color color = Color.white;


    public SpriteRenderer sr;
    public CardDataSO cardData;

    private IAttackBehavior behavior;
    
    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;
    }

    public void SetCardData(CardDataSO cData)
    {
        this.cardData = cData;
        ChangeCardSprite();
    }

    private void ChangeCardSprite()
    {
        sr.sprite = cardData.cardImage;
    }
    private void Update()
    {
        behavior?.Tick(this);
    }

    public void ChangeBehavior(IAttackBehavior newBehavior)
    {
        behavior = newBehavior;
    }

    public void AddModifier(IModifier modifier)
    {
        modifiers.Add(modifier);
        modifier.Apply(this);
    }

    public List<IModifier> GetModifiers() => modifiers;
}