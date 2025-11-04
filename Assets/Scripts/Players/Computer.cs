
using System.Collections.Generic;
using SOFile;
using UnityEngine;

public class Computer : IPlayer
{

    private static Computer instance;

    public static Computer GetInstance()
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

    public void Start()
    {
        playerData = Resources.Load<UserDataSO>("InGameData/OpponentHandSO");

        if (playerData == null)
        {
            Debug.LogError("[Player] Failed to load UserDataSO assets from Resources/UserData/");
        }

        playerData.ResetData();
    }
}
