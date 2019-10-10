using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameObject player;
	private CharacterMovement playerScript;

	[SerializeField]
	private Transform target;


	void Start()
	{
		Debug.Log("Starting");
		playerScript = (CharacterMovement)player.GetComponent(typeof(CharacterMovement));

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("IsColliding");

		player.transform.position = target.position;
		playerScript.StairsTransistion();

		//player.position = target.position;
		//GameObject.Find("Character").GetComponent<>().StairsTransistion();

		// this was used to diff the player
		//if (other.CompareTag("Player"))
		//{

		//}
	}
}
