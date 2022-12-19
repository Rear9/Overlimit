using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMP_Text text;
    public float score;
    private GameObject[] coins;
    public AudioSource coinpickup;

    private void Start()
    {
        score = 0;
        text = GetComponent<TMP_Text>();
        coins = GameObject.FindGameObjectsWithTag("coin");
    }
    private void Update()
    {
        text.text = "Coins: " + score + "/" + coins.Length;
        Debug.Log(score);
    }
}
