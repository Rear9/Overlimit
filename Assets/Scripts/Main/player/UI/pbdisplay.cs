using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pbdisplay : MonoBehaviour
{
    private float lvl1pb, lvl2pb, lvl3pb, lvl4pb, lvl5pb;
    private TMP_Text text;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void LateUpdate()
    {
        lvl1pb = PlayerPrefs.GetFloat("lvl1pb");
        lvl2pb = PlayerPrefs.GetFloat("lvl2pb");
        lvl3pb = PlayerPrefs.GetFloat("lvl3pb");
        //lvl4pb = PlayerPrefs.GetFloat("lvl4pb");
        //lvl5pb = PlayerPrefs.GetFloat("lvl5pb");

        if (lvl1pb == float.PositiveInfinity)
        {
            lvl1pb = float.NaN;
        }
        if (lvl2pb == float.PositiveInfinity)
        {
            lvl2pb = float.NaN;
        }
        
        if (lvl3pb == float.PositiveInfinity)
        {
            lvl3pb = float.NaN;
        }
        /*
        if (lvl4pb == float.PositiveInfinity)
        {
            lvl4pb = float.NaN;
        }
        if (lvl5pb == float.PositiveInfinity)
        {
            lvl5pb = float.NaN;
        }
        */


        text.text = "Personal Bests:" + "\n" + "\n" + "Level 1: " + lvl1pb + "s" + 
        "\n" + "\n" + "Level 2: " + lvl2pb + "s" +
        "\n" + "\n" + "Level 3: " + lvl3pb + "s";
    }
}
