using SOFile;
using UnityEngine;

public abstract class DestroyOnRoundEnd : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        GameEvents.OnEndOfRound += HandleRoundEnd;
    }

    protected virtual void OnDisable()
    {
        GameEvents.OnEndOfRound -= HandleRoundEnd;
    }

    protected virtual void HandleRoundEnd(CardDataSO smth)
    {
        Destroy(gameObject);
    }

    protected virtual void HandleRoundEnd(int smth)
    {
        Destroy(gameObject);
    }
}
