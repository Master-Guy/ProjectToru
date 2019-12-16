using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;

public class LevelManager : MonoBehaviour
{
    Scene scene;

    [SerializeField]
    string levelNameOverride = "";

    [SerializeField]
    int levelIndex = 0;

    [SerializeField]
    bool enableFPSMonitor = true;

    [SerializeField]
    GameObject FPSMonitor = null;

    static Dictionary<string, LevelCondition> conditions = new Dictionary<string, LevelCondition>();

    void Start()
    {
        //LevelDirector.Instance().Reset();
        scene = SceneManager.GetActiveScene();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    void Update()
    {
        if (enableFPSMonitor == true)
        {
            if (Input.GetKeyDown("tab"))
            {
                if (FPSMonitor != null)
                {
                    Debug.Log("Toggling FPS Monitor");
                    FPSMonitor.SetActive(!FPSMonitor.activeSelf);
                }
                else
                {
                    Debug.LogWarning("No FPS Monitor connected to level");
                }
            }
        }

        if (Input.GetKeyDown("t"))
        {
            foreach (LevelCondition condition in conditions.Values)
            {
                Debug.Log(condition.name + "\tRequired: " + condition.required + "\tFullfulled: " + condition.fullfilled + "\tFailed: " + condition.failed);
            }
        }
    }

    static LevelManager _instance;

    public static LevelManager Instance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }
        return _instance;
    }

    public static void AddCondition(LevelCondition condition)
    {

        if (condition.name == "")
        {
            Debug.LogError("Condition name parameter must be set");
            return;
        }

        if (Condition(condition.name) == null)
        {
            Debug.Log("Adding Condition '" + condition.name + "'");
            conditions.Add(condition.name, condition);
        }
        else
        {
            Debug.LogWarning("Condition with name '" + condition.name + "' already exists");
        }

    }

    public static void RemoveCondition(string conditionName)
    {

        if (conditionName == "")
        {
            Debug.LogError("Condition name cannot be empty");
            return;
        }

        if (Condition(conditionName) == null)
        {
            Debug.LogWarning("Condition '" + conditionName + "' that must be removed already does not exists");
            return;
        }

        conditions.Remove(conditionName);
        Debug.Log("Condition with name '" + conditionName + "' removed");

    }

    public static LevelCondition Condition(string name)
    {
        if (conditions.ContainsKey(name))
        {
            return conditions[name];
        }

        return null;
    }

    public static void FinishLevel()
    {
        Debug.LogWarning("DOET NOG NIKS");
    }

    void OnDestroy()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    //public void LevelEndSuccess()
    //{
    //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level" + levelIndex.ToString(), this.GetLevelName());
    //}

    //public void LevelEndFail()
    //{
    //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level" + levelIndex.ToString(), this.GetLevelName());
    //}

    public string GetLevelName()
    {
        return (levelNameOverride != "") ? levelNameOverride : scene.name;
    }

    public BuildingBehaviour GetBuilding()
    {
        return GetComponent<BuildingBehaviour>();
    }

}
