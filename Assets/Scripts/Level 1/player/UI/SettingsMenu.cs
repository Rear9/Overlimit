using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public camMovement camScript;
    public AudioMixer mixer;
    public float setFov, setSens;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDrop;
    
    void Start()
    {

        resolutions = Screen.resolutions;

        resolutionDrop.ClearOptions();
        List<string> reses = new List<string>();

        int currentResolution = 0;
        for (int i=0; i<resolutions.Length; i++)
        {
            string res = resolutions[i].width + " x " + resolutions[i].height;
            reses.Add(res);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }

        }
        resolutionDrop.AddOptions(reses);
        resolutionDrop.value = currentResolution;
        resolutionDrop.RefreshShownValue();
    }
    public void SetResolution(int resolution)
    {
        Resolution res = resolutions[resolution];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void FullScreen(bool full)
    {
        Screen.fullScreen = full;
    }

    public void SetVolume(float sliderVal)
    {
        PlayerPrefs.SetFloat("Volume", sliderVal);
        mixer.SetFloat("masterVolume", sliderVal);
        PlayerPrefs.Save();
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
}
