using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour
{
    public static MultiplayerSettings Instance { get; private set; }

    public bool delayedStart;
    public byte maxPlayerCount;

    public int menuSceneIndex;
    public int multiplayerSceneIndex;

    private void Awake()
    {
        // If there is already an instance then destroy it and replace with the new one
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            Debug.LogWarning("Another instance of MultiplayerSettings found, there should only be one in the scene");
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
