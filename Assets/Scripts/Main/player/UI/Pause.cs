using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool paused;
    public LevelComplete levelComplete;
    private float currentTimeScale;
    public GameObject pauseMenuUI;
    private SettingsMenu settingsMenu;

    private void Start()
    {
        settingsMenu = GetComponent<SettingsMenu>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (levelComplete.completed) return;

            if (!paused)
            {
                Paused();

            }
            else
            {
                settingsMenu.CloseSettings();
                UnPaused();
            }
        }
    }

    public void Paused()
    {
        currentTimeScale = Time.timeScale;
        paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void UnPaused()
    {
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = currentTimeScale;
        pauseMenuUI.SetActive(false);
    }

    public void MenuLoad()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void Continue()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Cursor.visible = false;
        }
        else
        {
            MenuLoad();
            PlayerPrefs.SetInt("Completed", 1);
        }
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
