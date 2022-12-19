using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Score score;
    public Enemies enemies;
    public bool completed;
    public GameObject levelCompleteUI;

    private void Start()
    {
        completed = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBase")){
            if (score.score == score.coinLength || enemies.enemyCount == 0)
            {
                completed = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                levelCompleteUI.SetActive(true);
            }
        }
    }
}
