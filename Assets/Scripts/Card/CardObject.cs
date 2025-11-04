using UnityEngine;
using SOFile;

public class CardObject : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private CardDataSO cardData;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public CardDataSO GetCardData()
    {
        return cardData;
    }
    void Update()
    {
        // 필요 시 카드 관련 애니메이션이나 상태 업데이트
    }

    public void AttachCardData(CardDataSO data)
    {
        cardData = data;
        DisplayCardImage();
    }

    public void DisplayCardImage()
    {
        Debug.Log(cardData);
        if (spriteRenderer != null && cardData != null)
            spriteRenderer.sprite = cardData.cardImage;
        else
            Debug.LogWarning("[CardObject] Missing SpriteRenderer or CardDataSO");
    }
}