using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Score score;
    public Timer timer;
    public double finalTime;
    public Enemies enemies;
    public bool completed;
    public GameObject levelCompleteUI;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBase")){
            if (score.score > 0 || enemies.enemyCount <= enemies.enemyCount-1)
            {
                completed = true;
                Time.timeScale = 0;
                finalTime = Math.Round(timer.time, 2);
                if (finalTime < PlayerPrefs.GetFloat("lvl1pb") && SceneManager.GetActiveScene().buildIndex == 1)
                {
                    PlayerPrefs.SetFloat("lvl1pb", (float)finalTime);
                    PlayerPrefs.Save();

                    Debug.Log("Level 1 PB: " + PlayerPrefs.GetFloat("lvl1pb"));
                }
                else if(finalTime<PlayerPrefs.GetFloat("lvl2pb") && SceneManager.GetActiveScene().buildIndex == 2)
                {
                    PlayerPrefs.SetFloat("lvl2pb", (float)finalTime);
                    PlayerPrefs.Save();

                    Debug.Log("Level 2 PB: " + PlayerPrefs.GetFloat("lvl2pb"));
                }
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                levelCompleteUI.SetActive(true);
            }
        }
    }
}
