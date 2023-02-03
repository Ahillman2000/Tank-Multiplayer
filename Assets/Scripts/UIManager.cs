using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject highscoreContainer;
    [SerializeField] private GameObject SettingsContainer;
    [SerializeField] private GameObject gameOverContaner;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning($"There should only be one instance of {Instance.GetType()}", this);
            Destroy(this);
        }
    }

    public void ToggleHighscoreContainer()
    {
        if(highscoreContainer.activeInHierarchy)
        {
            highscoreContainer.SetActive(false);
        }
        else
        {
            highscoreContainer.SetActive(true);
        }
    }

    public void ToggleSettingContainer()
    {
        if (highscoreContainer.activeInHierarchy)
        {
            SettingsContainer.SetActive(false);
        }
        else
        {
            SettingsContainer.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverContaner.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("OfflineGame");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
