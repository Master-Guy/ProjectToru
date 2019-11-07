using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{

	[SerializeField]
	Collider2D collider = null;

	bool closed = true;

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

		StartCoroutine(WaitForAnimationEndTimer());

		return true;
	}

	IEnumerator WaitForAnimationEndTimer()
	{
		yield return new WaitForSeconds(0.5f);
		collider.enabled = false;
	}

	public bool IsOpen()
	{
		return !closed;
	}

	public bool IsClosed()
	{
		return closed;
	}

	Room FindRoom()
	{
		return this.GetComponentInParent(typeof(Room)) as Room;
	}
}
