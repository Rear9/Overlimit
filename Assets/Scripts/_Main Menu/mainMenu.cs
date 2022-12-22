using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class mainMenu : MonoBehaviour
{
    public GameObject helpUI, LevelSelect;

    private void Start()
    {
    }
    public void Play()
    {
        if (PlayerPrefs.HasKey("lvl1pb") && PlayerPrefs.GetFloat("lvl1pb") != float.PositiveInfinity)
        {
            if (!LevelSelect.activeInHierarchy)
            {
                LevelSelect.SetActive(true);
            }
            else
            {
                LevelSelect.SetActive(false);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
        }
    }

    public void Load1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Load2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseHelp()
    {
        helpUI.SetActive(false);
    }

    public void OpenHelp()
    {
        helpUI.SetActive(true);
    }
}
