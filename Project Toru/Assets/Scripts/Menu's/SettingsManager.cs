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
        applyButton.onClick.AddListener(delegate { OnApplyClick(); });
        mainMenuButton.onClick.AddListener(delegate { OnCancelClick(); });

        resolutions = Screen.resolutions;

        LoadSettings();
    }

    //Full screen toggle
    public void OnFSToggle()
    {
        gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    //Resolution change in dropdown
    public void OnResChange()
    {

    }

    public void OnGeneralAudioChange()
    {
        generalSource.volume = gameSettings.generalAudioVolume = (int) generalAudioSlider.value;
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = (int)musicSlider.value;
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void OnApplyClick()
    {
        SaveSettings();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        generalAudioSlider.value = gameSettings.generalAudioVolume;
        musicSlider.value = gameSettings.musicVolume;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDropdown.value = gameSettings.resolutionIndex;
    }

    public void OnCancelClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
