using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject highscoreContainer;
    [SerializeField] private GameObject settingsContainer;
    [SerializeField] private GameObject gameOverContaner;

    [SerializeField] private InputManager inputManager;

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

    void Start()
    {
        inputManager.tankInputActions.Tank.ToggleSettings.performed += DoToggleSettingContainer;
    }

    private void OnDisable()
    {
        inputManager.tankInputActions.Tank.ToggleSettings.performed -= DoToggleSettingContainer;
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

    private void DoToggleSettingContainer(InputAction.CallbackContext context)
    {
        ToggleSettingContainer();
    }

    public void ToggleSettingContainer()
    {
        if (settingsContainer.activeInHierarchy)
        {
            settingsContainer.SetActive(false);
        }
        else
        {
            settingsContainer.SetActive(true);
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
