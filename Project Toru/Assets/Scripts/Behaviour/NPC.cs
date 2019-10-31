using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	private INPC npcObject;

	[SerializeField]
	private NPCinfo info;

	private npcType type;
	private npcState state;

	public Animator animator { get; }

	// Start is called before the first frame update
	void Start()
    {
		type = info.type;
		state = info.state;

		switch (type)
		{
			case npcType.Employee:
				npcObject = new Employee(this);
				break;
			case npcType.Guard:
				npcObject = new Guard();
				break;
			default:
				Debug.Log("Type of NPC does not exist");
				break;
		}
	}

    // Update is called once per frame
    void Update()
    {
		switch (state)
		{
			case npcState.Idle:
				npcObject.Idle();
				break;
			case npcState.Surrender:
				npcObject.Surrender();
				break;
			case npcState.Defend:
				npcObject.Defend();
				break;
			case npcState.Flee:
				npcObject.Flee();
				break;
		}
    }

	void OnMouseDown()
	{
		npcObject.Surrender();
	}
}

public enum npcType
{
	Employee,
	Guard
}

public enum npcState
{
	None,
	Idle,
	Surrender,
	Defend,
	Flee
}
