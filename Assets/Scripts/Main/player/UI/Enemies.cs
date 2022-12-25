using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemies : MonoBehaviour
{
    private TMP_Text text;
    public float enemyCount;
    public float initialCount;
    public float kills;
    private GameObject[] enemies;
    void Start()
    {
        enemyCount = 0;
        initialCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        kills = initialCount - enemyCount;
        text.text = "Enemies: " + enemyCount;
    }
}
