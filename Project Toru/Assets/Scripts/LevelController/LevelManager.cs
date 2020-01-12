using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameAnalyticsSDK;

/// <summary>
/// This Level Manager receives "conditions" from the game.
/// Any condtions can have a callback when a condition is met or not. Conditions don't check themselves, because the all behave differently
/// </summary>
///
/// <example>
///
/// LevelConditionInt condition = new LevelConditionInt();
/// condition.name = "AShortDiscriptionWhatThisConditionMustFullfull";
/// condition.fullfillHandler = (LevelCondition c) =>
/// {
///     // Logic when condition is fullfulled
///     LevelManager.EndLevel("Title", "Message", [seconds to end]);
/// };
/// LevelManager.AddCondition(condition);
///
/// Then, use
/// LevelManager.Condition("AShortDiscriptionWhatThisConditionMustFullfull").fullfill() or .fail
/// anywere. 
/// 
/// </example>
public class LevelManager : MonoBehaviour
{

    /// <summary>
    /// The current scene
    /// </summary>
    Scene scene;

    /// <summary>
    /// Override scene name, default = scene file name
    /// </summary>
    [SerializeField]
    string levelNameOverride = "";

    /// <summary>
    /// What level is this
    /// </summary>
    [SerializeField]
    public int levelIndex = 1;

    /// <summary>
    /// When enabled, press [tab] to see FPS monitor
    /// </summary>
    [SerializeField]
    bool enableFPSMonitor = true;

    [SerializeField]
    GameObject FPSMonitor = null;

    /// <summary>
    /// Storage for Level Conditions
    /// </summary>
    static Dictionary<string, LevelCondition> conditions = new Dictionary<string, LevelCondition>();

	void Awake() {
		if (!GameAnalytics.IsInitialized())
		{
			GameAnalytics.Initialize();
		}
	}
	
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level" + levelIndex.ToString(), this.GetLevelName());
    }

    void Update()
    {
        /// FPS Monitor
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

        /// Print current conditions in debug
        if (Input.GetKeyDown("t"))
        {
            Debug.Log("Printing current condition status");
            foreach (LevelCondition condition in conditions.Values)
            {
                Debug.Log(condition.name + "\tFullfulled: " + condition.fullfilled + "\tFailed: " + condition.failed);
            }
        }
    }


    /// <summary>
    /// When reaching an non static LevelManager method, use Instance().
    /// </summary>
    static LevelManager _instance;

    public static LevelManager Instance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }
        return _instance;
    }

    /// <summary>
    /// Adds a condition to database
    /// </summary>
    /// <param name="condition">Condtion</param>
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

            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level" + Instance().levelIndex.ToString(), Instance().GetLevelName(), condition.name);
        }
        else
        {
            Debug.LogWarning("Condition with name '" + condition.name + "' already exists");
        }

    }

    /// <summary>
    /// Remove a condition. Only needed when something is "undone"
    /// </summary>
    /// <param name="conditionName"></param>
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

    /// <summary>
    /// Get a condition by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Condition when found, otherwise NULL</returns>
    public static LevelCondition Condition(string name)
    {
        if (conditions.ContainsKey(name))
        {
            return conditions[name];
        }

        return null;
    }

    /// <summary>
    /// End level. For winning and losing
    /// The level is automaticly "won" when none of the conditions are failed.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="FailAfterSeconds">The to wait before ending the level</param>
    public static void EndLevel(string title, string message, float FailAfterSeconds = 0f)
    {
        Instance().StartCoroutine(SegueToFailScene(title, message, FailAfterSeconds));
    }

    /// <summary>
    /// Actualy ending the level.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="FailAfterSeconds"></param>
    /// <returns></returns>
    private static IEnumerator SegueToFailScene(string title, string message, float FailAfterSeconds)
    {
        if (AnyConditionFailed())
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "level" + Instance().levelIndex.ToString(), Instance().GetLevelName(), "main");
        }
        else
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level" + Instance().levelIndex.ToString(), Instance().GetLevelName(), "main");
        }

        yield return new WaitForSeconds(FailAfterSeconds);

        LevelEndMessage.title = title;
        LevelEndMessage.message = message;
        SceneManager.LoadScene("Fail");
    }


    /// <summary>
    /// Check if all conditions are fullfilled
    /// </summary>
    /// <returns></returns>
    public static bool AllConditionsFullfilled()
    {
        foreach (var condition in conditions.Values)
        {
            if (!condition.fullfilled) return false;
        }

        return true;
    }

    /// <summary>
    /// Check if any condition is failed
    /// </summary>
    /// <returns></returns>
    public static bool AnyConditionFailed()
    {

        foreach (var condition in conditions.Values)
        {
            if (condition.failed) return true;
        }

        return false;
    }

    /// <summary>
    /// Get level name
    /// </summary>
    /// <returns></returns>
    public string GetLevelName()
    {
        return (levelNameOverride != "") ? levelNameOverride : scene.name;
    }

    public BuildingBehaviour GetBuilding()
    {
        return GetComponent<BuildingBehaviour>();
    }

}