using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsConfig : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public Slider volumeSlider;
    
    public TMPro.TMP_Dropdown resolutionDropdown;
    
    Resolution[] resolutions;
    
    // NOTE RESOLUTION AND FULLSCREEN SETTINGS WILL NOT SHOW UNLESS GAME IS BUILT
    // Todo: Currently settings do not save to the next scene (aka they do not persist)

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumeDB", 10f);
        
        
        volumeSlider.minValue = -10f;
        volumeSlider.maxValue = 20f;
        volumeSlider.value = savedVolume;
        
        SetVolume(savedVolume);
        
        resolutions = Screen.resolutions;
        
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetVolume(float dB)
    {
        audioMixer.SetFloat("volume", dB);
        PlayerPrefs.SetFloat("VolumeDB", dB);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}
