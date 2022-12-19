using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class State : MonoBehaviour
{
    private TMP_Text text;
    public movement movement;

    // Update is called once per frame

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = "State: " + movement.state;
    }
}
