using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CompleteData : MonoBehaviour
{
    public Score score;
    public Enemies enemies;
    private TMP_Text text;
    private float pb;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            pb = PlayerPrefs.GetFloat("lvl1pb");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            pb = PlayerPrefs.GetFloat("lvl2pb");
        }
        /*
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            pb = PlayerPrefs.GetFloat("lvl3pb");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            pb = PlayerPrefs.GetFloat("lvl4pb");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            pb = PlayerPrefs.GetFloat("lvl5pb");
        }
        */

    }

    private void LateUpdate()
    {
        text.text = "Level " + SceneManager.GetActiveScene().buildIndex + " Complete!" + "\n" + "\n" + "PB: " + pb + " seconds." + "\n" + "Coins Collected: " + score.score + "/" + score.coinLength + "\n" + "Enemies Remaining: " + enemies.enemyCount;
    }
}
