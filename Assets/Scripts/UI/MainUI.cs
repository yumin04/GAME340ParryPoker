using UnityEngine;

public class MainUI : MonoBehaviour
{

    private static MainUI instance;
    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public static MainUI GetInstance() => instance;
    
}