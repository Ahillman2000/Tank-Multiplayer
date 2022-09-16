using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadOfflineGame()
    {
        SceneManager.LoadScene("OfflineGame");
    }

    public void LoadOnlineGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Quit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
