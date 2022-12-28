using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class mainMenu : MonoBehaviour
{
    public GameObject helpUI, credits;
    public GameObject[] levelSelects;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Completed") == 1)
        {
            credits.SetActive(true);
        }
    }
    public void Play()
    {
        if (PlayerPrefs.HasKey("lvl1pb") && PlayerPrefs.GetFloat("lvl1pb") != float.PositiveInfinity)
        {
            if (!levelSelects[0].activeInHierarchy || !levelSelects[1].activeInHierarchy)
            {
                levelSelects[0].SetActive(true);
                levelSelects[1].SetActive(true);
            }
            else
            {
                levelSelects[0].SetActive(false);
                levelSelects[1].SetActive(false);
            }

            if(PlayerPrefs.HasKey("lvl2pb") && PlayerPrefs.GetFloat("lvl2pb") != float.PositiveInfinity){
                if (!levelSelects[2].activeInHierarchy)
                {
                    levelSelects[2].SetActive(true);
                }
                else
                {
                    levelSelects[2].SetActive(false);
                }
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
    public void Load3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        Time.timeScale = 1;
    }

    public void Load4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
        Time.timeScale = 1;
    }
    public void Load5()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
        Time.timeScale = 1;
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void ResetStats()
    {
        PlayerPrefs.SetFloat("lvl1pb", float.PositiveInfinity);
        PlayerPrefs.SetFloat("lvl2pb", float.PositiveInfinity);
        PlayerPrefs.SetFloat("lvl3pb", float.PositiveInfinity);
        PlayerPrefs.SetFloat("lvl4pb", float.PositiveInfinity);
        PlayerPrefs.SetFloat("lvl5pb", float.PositiveInfinity);
        PlayerPrefs.SetFloat("Fov", 75);
        PlayerPrefs.SetFloat("Sens", 250);
        PlayerPrefs.SetFloat("Volume", 1);
        PlayerPrefs.SetInt("Completed", 0);
    }
    public void CloseHelp()
    {
        helpUI.SetActive(false);
    }

    public void OpenHelp()
    {
        helpUI.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
    }
}
