using Assets.Domain.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
	[SerializeField]
	private Room roomLeft = null, roomRight = null;

	[SerializeField]
	private int key;

    private bool closed = true;

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player"))
            if (collision.gameObject.GetComponent<Character>().HasKey(key) && closed)
                OpenDoor();
	}

    private void OpenDoor()
    {
        Debug.Log("Opening door");
        closed = false;
        GetComponent<Animator>().SetBool("openDoor", true);
        GetComponent<Collider2D>().enabled = false;
    }
}
