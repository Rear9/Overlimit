using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Slider fovSlider, sensSlider, volumeSlider;
    public TMP_Dropdown qualityDrop, resDrop;
    public Toggle fullscreen;
    private void Update()
    {
        if (!PlayerPrefs.HasKey("Fov"))
        {
            PlayerPrefs.SetFloat("Fov", 75);
        }

        if (!PlayerPrefs.HasKey("Sens"))
        {
            PlayerPrefs.SetFloat("Sens", 250);
        }


        PlayerPrefs.SetFloat("Fov", fovSlider.value);
        PlayerPrefs.SetFloat("Sens", sensSlider.value);


        if (!PlayerPrefs.HasKey("lvl1pb"))
        {
            PlayerPrefs.SetFloat("lvl1pb", float.PositiveInfinity);
        }
        if (!PlayerPrefs.HasKey("lvl2pb"))
        {
            PlayerPrefs.SetFloat("lvl2pb", float.PositiveInfinity);
        }
        /*
        if (!PlayerPrefs.HasKey("lvl3pb"))
        {
            PlayerPrefs.SetFloat("lvl3pb", float.PositiveInfinity);
        }
        if (!PlayerPrefs.HasKey("lvl4pb"))
        {
            PlayerPrefs.SetFloat("lvl4pb", float.PositiveInfinity);
        }
        if (!PlayerPrefs.HasKey("lvl5pb"))
        {
            PlayerPrefs.SetFloat("lvl5pb", float.PositiveInfinity);
        }
        */
        PlayerPrefs.Save();
    }
}
