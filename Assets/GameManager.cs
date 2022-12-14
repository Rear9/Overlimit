using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider fovSlider, sensSlider, volumeSlider;

    private void Update()
    {
        PlayerPrefs.SetFloat("Fov", fovSlider.value);
        PlayerPrefs.SetFloat("Sens", sensSlider.value);
        PlayerPrefs.Save();
    }
}
