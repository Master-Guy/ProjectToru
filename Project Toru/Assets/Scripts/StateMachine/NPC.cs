using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	private StateMachine statemachine;

	private Vector3 startingPosition;

	private Animator animator;

	private Room currentRoom;

	[SerializeField]
	private GameObject TextBox;

	[SerializeField]
	private GameObject[] escapePath;

	private bool surrender = false;

	[SerializeField]
	private GameObject[] bag;

	void Start()
	{
		startingPosition = transform.position;
		animator = GetComponent<Animator>();
		statemachine = new StateMachine();

		PingPong();
	}

	void Update()
	{
		this.statemachine.ExecuteStateUpdate();
		AdjustOrderLayer();
		StateCheck();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject.GetComponent<Room>();
		}
	}

	void OnMouseDown()
	{
		Surrender();
	}

	private void StateCheck()
	{
		if(!currentRoom.AnyCharacterInRoom() && statemachine.GetCurrentlyRunningState().ToString() == "Surrender" && surrender)
		{
			surrender = false;
			this.statemachine.ChangeState(new Flee(this.escapePath, this.gameObject, this.animator));
		}
	}

	public void PingPong()
	{
		this.statemachine.ChangeState(new PingPong(this.startingPosition, this.gameObject, this.animator));
	}
	public void Surrender()
	{
		if (currentRoom.SelectedPlayerInRoom() && !surrender)
		{
			surrender = true;
			this.statemachine.ChangeState(new Surrender(this.animator, this.gameObject, this.currentRoom));
			dropBag();
		}
	}

	public void dropBag()
	{
		if(bag.Length > 0)
		{
			foreach(GameObject g in bag)
			{
				Instantiate(g, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, 0), Quaternion.identity);
			}
		}
	}

	public void Say(string text)
	{
		TextBox.GetComponent<TextMesh>().text = text;
		TextBox.SetActive(true);
		Invoke("disableTextBox", 3);
	}
	private void disableTextBox()
	{
		TextBox.SetActive(false);
	}
	private void AdjustOrderLayer()
	{
		GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 1000);
	}

	public StateMachine getStateMachine()
	{
		return this.statemachine;
	}
}
