using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Speed : MonoBehaviour
{
    private TMP_Text text;
    public Rigidbody rb;
    public movement movement;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = "Speed: " + Math.Round(rb.velocity.magnitude, 2);
    }
}
