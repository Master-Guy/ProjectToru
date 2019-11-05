﻿using Assets.Domain.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{

	[SerializeField]
	Collider2D collider;

	[SerializeField]
	int key;

	bool closed = true;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			if (collision.gameObject.GetComponent<Character>().hasKey(key) && closed)
				Open();
	}

	public bool Close()
	{
		Debug.Log("Closing door");
		closed = true;
		GetComponent<Animator>().SetBool("openDoor", false);
		collider.enabled = true;

		return true;
	}

	public bool Open()
	{
		Debug.Log("Opening door");
		closed = false;
		GetComponent<Animator>().SetBool("openDoor", true);
		collider.enabled = false;

		return true;
	}

	public bool IsOpen()
	{
		return !closed;
	}

	public bool IsClosed()
	{
		return closed;
	}
}
