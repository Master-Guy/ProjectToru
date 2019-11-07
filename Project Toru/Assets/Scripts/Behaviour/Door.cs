using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{

	[SerializeField]
	Collider2D collider = null;

	[SerializeField]
	int key = -1;

	bool closed = true;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			if (collision.gameObject.GetComponent<Character>().HasKey(key) && closed)
				Open();
	}

	public bool Close()
	{
		Debug.Log("Closing door");
		closed = true;
		GetComponent<Animator>().SetBool("openDoor", false);
		collider.enabled = true;

		FindRoom().GetCardReaderRight().SetStatus(false);
		FindRoom().RightRoom.GetCardReaderLeft().SetStatus(false);

		return true;
	}

	public bool Open()
	{
		Debug.Log("Opening door");
		closed = false;
		GetComponent<Animator>().SetBool("openDoor", true);

		FindRoom()?.GetCardReaderRight()?.SetStatus(true);
		FindRoom()?.RightRoom?.GetCardReaderLeft()?.SetStatus(true);

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
