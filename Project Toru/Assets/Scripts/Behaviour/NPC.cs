using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private npcState currentState = npcState.defaultAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case npcState.defaultAction:
                break;
            case npcState.surrendering:
                break;
            case npcState.fleeing:
                break;
            default:
                break;
        }
    }
}

public enum npcState
{
    defaultAction,
    surrendering,
    fleeing
}
