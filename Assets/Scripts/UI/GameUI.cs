using UnityEditorInternal;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject userSelectActionPanel;
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


    public void EnableUserSelectActionPanel()
    {
        userSelectActionPanel.SetActive(true);
    }

    public void DisableUserSelectActionPanel()
    {
        userSelectActionPanel.SetActive(false);
    }

}
