using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text text;
    public float time = 0;

    private void Start()
    {
        time = 0;
        text = GetComponent<TMP_Text>();
    }
    private void Update()
    {
        time += Time.deltaTime;
        text.text = "" + Math.Round(time, 1);

    }
}
