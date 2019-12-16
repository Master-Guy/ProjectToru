using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour
{
    public Button mainMenuButton;
    public Button LevelOneButton;
    public Button LevelTwoButton;
    public Button LevelThreeButton;
    public Button LevelFourButton;
    public Button LevelFiveButton;
    public Button LevelSixButton;

    private void Start()
    {
        //mainMenuButton.onClick.AddListener(delegate { OnMainMenuClick(); });
        LevelOneButton.onClick.AddListener(delegate { OnLevelOneClick(); });
        LevelTwoButton.onClick.AddListener(delegate { OnLevelTwoClick(); });
        LevelThreeButton.onClick.AddListener(delegate { OnLevelThreeClick(); });

        LevelOneButton.onClick.AddListener(delegate { OnLevelClick("One"); });
        LevelTwoButton.onClick.AddListener(delegate { OnLevelClick("Two"); });
        LevelThreeButton.onClick.AddListener(delegate { OnLevelClick("Three"); });
        LevelFourButton.onClick.AddListener(delegate { OnLevelClick("Four"); });
        LevelFiveButton.onClick.AddListener(delegate { OnLevelClick("Five"); });
        LevelSixButton.onClick.AddListener(delegate { OnLevelClick("Six"); });
    }

    public void OnLevelClick(string s)
    {
        string sceneName = "Level" + s;
        Debug.Log(sceneName);

        SceneManager.LoadScene(sceneName);
    }

    public void OnLevelOneClick()
    {
        SceneManager.LoadScene("NewStairs");
    }

    public void OnLevelTwoClick()
    {
        SceneManager.LoadScene("NewStairs");
    }

    public void OnLevelThreeClick()
    {
        SceneManager.LoadScene("NewStairs");
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
