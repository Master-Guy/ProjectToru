using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameAnalyticsSDK;

public delegate void ConditionHandlerDelegate(LevelCondition condition);

public class LevelDirector : MonoBehaviour
{

    //// SINGLETON
    //// Making LevelDirector unable to instanciate
    //private LevelDirector()
    //{

    //}

    //// Static LevelDirector instance
    //private static LevelDirector instance = new LevelDirector();

    //// Function to get instance
    //public static LevelDirector Instance()
    //{
    //    return instance;
    //}


    //public void Reset()
    //{
    //    Debug.Log("Resetting Conditions");
    //    conditions.Clear();
    //}


    static Dictionary<string, LevelCondition> conditions = new Dictionary<string, LevelCondition>();

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

    }
}
