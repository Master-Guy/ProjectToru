using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameAnalyticsSDK;

public delegate void ConditionHandlerDelegate(LevelCondition condition);

public class LevelDirector
{

    // SINGLETON
    // Making LevelDirector unable to instanciate
    private LevelDirector()
    {

    }

    // Static LevelDirector instance
    private static LevelDirector instance = new LevelDirector();

    // Function to get instance
    public static LevelDirector Instance()
    {
        return instance;
    }


    //public void Reset()
    //{
    //    Debug.Log("Resetting Conditions");
    //    conditions.Clear();
    //}


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

    public void FinishLevel()
    {

    }
}
