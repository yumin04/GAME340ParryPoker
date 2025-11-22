using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    
    public static SceneLoader GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

    // Loading all scene names4
    // public void Start()
    // {
    //     int sceneCount = SceneManager.sceneCountInBuildSettings;
    //     for (int i = 0; i < sceneCount; i++)
    //     {
    //         string path = SceneUtility.GetScenePathByBuildIndex(i);
    //         string name = System.IO.Path.GetFileNameWithoutExtension(path);
    //         Debug.Log(name);
    //     }
    // }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void ExitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}