using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider generalAudioSlider;
    public Slider musicSlider;
    public Button applyButton;

    public AudioSource generalSource;
    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;
    public string test;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFSToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResChange(); });
        generalAudioSlider.onValueChanged.AddListener(delegate { OnGeneralAudioChange(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyClick(); });

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }

    //Full screen toggle
    public void OnFSToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    //Resolution change in dropdown
    public void OnResChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }

    public void OnGeneralAudioChange()
    {
        generalSource.volume = generalAudioSlider.value;
        gameSettings.generalAudioVolume = generalAudioSlider.value;
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = musicSlider.value;
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void OnApplyClick()
    {
        SaveSettings();
    }

    public void LoadSettings()
    {

    }
}
