using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMP_Text text;
    public int score, coinLength;
    private GameObject[] coins;
    public AudioSource coinpickup;

    private void Start()
    {
        score = 0;
        text = GetComponent<TMP_Text>();
        coins = GameObject.FindGameObjectsWithTag("coin");
        coinLength = coins.Length;
    }
    private void Update()
    {
        text.text = "Coins: " + score + "/" + coinLength;
    }
}
