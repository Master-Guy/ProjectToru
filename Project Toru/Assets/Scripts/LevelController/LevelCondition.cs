using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCondition
{
    public LevelCondition()
    {

    }

    public LevelCondition(bool required)
    {
        this.required = required;
    }

    // Name instance
    private string _name = "";
    public string name
    {
        get
        {
            return _name;
        }
        set
        {
            if (_name != "")
            {
                Debug.LogWarning("LevelCondition name cannot be changed, define a new one.");
                return;
            }

            if (value == "")
            {
                Debug.LogWarning("LevelCondition name can not be empty.");
                return;
            }

            if (LevelDirector.Instance().Condition(value) != null)
            {
                Debug.LogWarning("LevelCondition name already used.");
                return;
            }

            _name = value;
        }
    }


    public bool required = false;

    private bool _fullfilled = false;
    public bool fullfilled
    {
        get
        {
            return _fullfilled;
        }
    }

    private bool _failed = false;
    public bool failed
    {
        get
        {
            return _failed;
        }
    }
    //public bool endOnFail = false;

    public ConditionHandlerDelegate fullfillHandler = null;
    public ConditionHandlerDelegate failHandler = null;

    public void Fullfill()
    {
        Debug.Log(name + " fullfilled");
        _fullfilled = true;

        fullfillHandler?.Invoke(this);
    }

    public void Fail()
    {
        Debug.Log(name + " failed");
        _failed = true;

        failHandler?.Invoke(this);
    }

    public void Commit()
    {
        LevelDirector.Instance().AddCondition(this);
    }


    public void Revoke()
    {
        LevelDirector.Instance().RemoveCondition(this.name);
    }
}