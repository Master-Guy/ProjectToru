using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : NPC
{

    void Start()
    {

	}

    void Update()
    {
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();
	}

	void OnMouseDown()
	{
		Debug.Log("Guard");
	}
}
