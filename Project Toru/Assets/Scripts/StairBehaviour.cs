using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBehaviour : MonoBehaviour
{
	[SerializeField]
	private Transform player;

	[SerializeField]
	private Transform target;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("IsColliding");

		player.position = target.position;

		// this was used to diff the player
		//if (other.CompareTag("Player"))
		//{

		//}
	}
}
