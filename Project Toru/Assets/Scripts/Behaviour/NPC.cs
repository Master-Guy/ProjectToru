using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	private INPC npcObject;

	[SerializeField]
	private NPCinfo info;

	public List<GameObject> bag;

	private npcType type;
	private npcState state;

	public Animator animator;

	private GameObject currentRoom;

	public Vector3 startingPosition;
	public Vector3 change;

	private bool moving = true;

	// Start is called before the first frame update
	void Start()
    {
		type = info.type;
		state = info.state;
		animator = GetComponent<Animator>();
		animator.runtimeAnimatorController = info.animatorController;

		startingPosition = transform.position;
		change = Vector3.zero;

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
			case npcState.None:
				break;
			case npcState.Idle:
				npcObject.Idle();
				Move();
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
		state = npcState.None;

		if(currentRoom != null)
		{
			Debug.Log(currentRoom.ToString());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject;
		}
	}

	void Move()
	{
		if(transform.position != change)
		{
			animator.SetFloat("changeX", change.x - transform.position.x);
			animator.SetFloat("changeY", change.y - transform.position.y);
			animator.SetBool("isMoving", moving);
			transform.position = change;
			AdjustOrderLayer();
		}
	}

	void AdjustOrderLayer()
	{
		GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 1000);
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
