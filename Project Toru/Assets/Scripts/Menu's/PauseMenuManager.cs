using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPrefab;
    private Canvas _c;

    private Canvas SceneCanvas
    {
        get
        {
            if (_c == null)
            {
                _c = FindObjectOfType<Canvas>();

                if (_c == null)
                {
                    _c = Instantiate(new Canvas());
                }
            }

            return _c;
        }
    }

    private GameObject pauseMenuInstance;

   public void OpenMenu()
    {
        if (pauseMenuInstance == null)
        {
            Instantiate(pauseMenuPrefab, SceneCanvas.transform);
        } else
        {
            pauseMenuInstance.SetActive(true);
        }
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            Time.timeScale = 0;
            OpenMenu();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
