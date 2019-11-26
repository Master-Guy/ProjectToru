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

    private void Start()
    {
        mainMenuButton.onClick.AddListener(delegate { OnMainMenuClick(); });
        LevelOneButton.onClick.AddListener(delegate { OnLevelOneClick(); });
        LevelTwoButton.onClick.AddListener(delegate { OnLevelTwoClick(); });
        LevelThreeButton.onClick.AddListener(delegate { OnLevelThreeClick(); });
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
