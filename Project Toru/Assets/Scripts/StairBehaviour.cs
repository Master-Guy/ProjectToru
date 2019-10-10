using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBehaviour : MonoBehaviour
{
	[SerializeField]
	private GameObject player;
	private Character playerScript;

	[SerializeField]
	private Transform target;


	void Start()
	{
		Debug.Log("Starting");
		playerScript = (Character)player.GetComponent(typeof(Character));

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
