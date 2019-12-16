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