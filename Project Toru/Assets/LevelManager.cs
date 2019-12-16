using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;


public delegate void ConditionHandlerDelegate(LevelCondition condition);

public class LevelDirector
{

    // Making LevelDirector unable to instanciate
    private LevelDirector()
    {

    }

    private static LevelDirector instance = new LevelDirector();

    public void Reset()
    {
        Debug.Log("Resetting Conditions");
        conditions.Clear();
    }

    public static LevelDirector Instance()
    {
        return instance;
    }

    Dictionary<string, LevelCondition> conditions = new Dictionary<string, LevelCondition>();


    public void AddCondition(LevelCondition condition)
    {

        if (condition.name == "")
        {
            Debug.LogError("Condition name parameter must be set");
            return;
        }

        if (this.Condition(condition.name) == null)
        {
            Debug.Log("Adding Condition");
            conditions.Add(condition.name, condition);
        }
        else
        {
            Debug.LogWarning("Condition with name '" + condition.name + "' already exists");
        }

    }

    public LevelCondition Condition(string name)
    {
        if (conditions.ContainsKey(name))
        {
            return conditions[name];
        }

        return null;
    }

    public bool ValidateConditions()
    {

        // When no conditions are set, we don't want the level to complete
        if (conditions.Count == 0)
        {
            return false;
        }

        //foreach (KeyValuePair<string, LevelCondition> condition in conditions)
        //{
        //    if (condition.Value.required == true && condition.Value.fullfilled == false)
        //}

        return false;
    }
}

public class LevelCondition
{
    public LevelCondition()
    {

    }

    public LevelCondition(bool required)
    {
        this.required = required;
    }

    public string name = "";
    public bool required = false;

    private bool fullfilled = false;
    private bool failed = false;
    //public bool endOnFail = false;

    public ConditionHandlerDelegate fullfillHandler = null;
    public ConditionHandlerDelegate failHandler = null;

    public void Fullfill()
    {
        Debug.Log(name + " fullfilled");
        fullfilled = true;

        if (fullfillHandler != null)
        {
            fullfillHandler(this);
        }
    }

    public void Fail()
    {
        Debug.Log(name + " failed");
        failed = true;

        if (failHandler != null)
        {
            failHandler(this);
        }
    }
}

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
    }

    void OnDestroy()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    public void LevelEndSuccess()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    public void LevelEndFail()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    public string GetLevelName()
    {
        return (levelNameOverride != "") ? levelNameOverride : scene.name;
    }

    public NPC[] GetNPCS()
    {
        return GetComponents<NPC>();
    }

    public Character[] GetCharacters()
    {
        return GetComponents<Character>();
    }

    public BuildingBehaviour GetBuilding()
    {
        return GetComponent<BuildingBehaviour>();
    }

}
