using UnityEngine;

public abstract class IUserInterface : MonoBehaviour
{
    private static IUserInterface instance;
    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static IUserInterface GetInstance() => instance;

    public abstract void HandlePlayer1MainAction();
    public abstract void HandlePlayer1Up();
    public abstract void HandlePlayer1Down();
    public abstract void HandlePlayer1Left();
    public abstract void HandlePlayer1Right();
    
    public abstract void HandlePlayer2MainAction();
    public abstract void HandlePlayer2Up();
    public abstract void HandlePlayer2Down();
    public abstract void HandlePlayer2Left();
    public abstract void HandlePlayer2Right();

    public abstract void HandleEditorCheatKey();
    public abstract void HandleEditorPauseKey();
}