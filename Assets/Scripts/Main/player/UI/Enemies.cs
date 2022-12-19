using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemies : MonoBehaviour
{
    private TMP_Text text;
    public float enemyCount;
    private GameObject[] enemies;
    void Start()
    {
        enemyCount = 0;
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        text.text = "Enemies: " + enemyCount;
    }
}
