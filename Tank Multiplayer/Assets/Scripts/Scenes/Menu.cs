using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadModeSelection()
    {
        SceneManager.LoadScene("ModeSelection");
    }

    public void loadOfflineGame()
    {
        SceneManager.LoadScene("OfflineGame");
    }

    public void LoadOnlineGame()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void Quit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
