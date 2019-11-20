using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider generalAudioSlider;
    public Slider musicSlider;
    public Button applyButton;
    public Button mainMenuButton;

    public AudioSource generalSource;
    public AudioSource musicSource;
    public Resolution[] resolutions;
    public GameSettings gameSettings;

    void OnEnable()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFSToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResChange(); });
        generalAudioSlider.onValueChanged.AddListener(delegate { OnGeneralAudioChange(); });
        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        mainMenuButton.onClick.AddListener(delegate { OnCancelClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution r in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(r.ToString()));
        }

        LoadSettings();
    }

    //Full screen toggle
    public void OnFSToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
        SaveSettings();
    }

    //Resolution change in dropdown
    public void OnResChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
        SaveSettings();
    }

    public void OnGeneralAudioChange()
    {
        generalSource.volume = gameSettings.generalAudioVolume = (int) generalAudioSlider.value;
        SaveSettings();
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = (int)musicSlider.value;
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        if(!File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            gameSettings.fullscreen = true;
            gameSettings.resolutionIndex = 21;
            gameSettings.generalAudioVolume = 100;
            gameSettings.musicVolume = 100;
            gameSettings.resolutionIndex = 0;

            string jsonData = JsonUtility.ToJson(gameSettings, true);
            File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
        }

        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        generalAudioSlider.value = gameSettings.generalAudioVolume;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        musicSlider.value = gameSettings.musicVolume;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDropdown.value = gameSettings.resolutionIndex;

        
    }

    public void OnCancelClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
