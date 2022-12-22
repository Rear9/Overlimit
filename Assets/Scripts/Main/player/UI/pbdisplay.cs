using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pbdisplay : MonoBehaviour
{
    private float lvl1pb, lvl2pb;
    private TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void LateUpdate()
    {
        lvl1pb = PlayerPrefs.GetFloat("lvl1pb");
        lvl2pb = PlayerPrefs.GetFloat("lvl2pb");
        if (lvl1pb == float.PositiveInfinity)
        {
            lvl1pb = float.NaN;
        }
        if (lvl2pb == float.PositiveInfinity)
        {
            lvl2pb = float.NaN;
        }
        text.text = "Personal Bests:" + "\n" + "\n" + "Level 1: " + lvl1pb + "s" + "\n" + "\n" + "Level 2: " + lvl2pb + "s";
    }
}
