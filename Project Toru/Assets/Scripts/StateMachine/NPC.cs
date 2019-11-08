using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NPC : MonoBehaviour
{
	[SerializeField]
	private GameObject[] bag;

	[SerializeField]
	private GameObject TextBox;

	[SerializeField]
	public GameObject[] escapePath;

	[NonSerialized]
	public Room currentRoom;

	[NonSerialized]
	public StateMachine statemachine = new StateMachine();

	[NonSerialized]
	public Vector3 startingPosition;

	[NonSerialized]
	public Animator animator;

    public void dropBag()
	{
		foreach (GameObject g in bag)
		{
			Instantiate(g, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.5f, 0), Quaternion.identity);
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

	public void AdjustOrderLayer()
	{
		GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 1000);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject.GetComponent<Room>();
		}
	}
}
