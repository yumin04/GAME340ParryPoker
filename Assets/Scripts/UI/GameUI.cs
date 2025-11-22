
using UnityEngine;

public class GameUI : MonoBehaviour
{

    private static GameUI instance;
    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public static GameUI GetInstance() => instance;
    
}
