using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBehaviour : MonoBehaviour
{
	// Assign target for this transistion. The character moves to this spot.
	[SerializeField]
	private Transform target;

	void OnTriggerEnter2D(Collider2D other)
	{
		// Move character
		other.transform.position = target.position;

		// Let character know it is using a stairs
		// Get GameObject from collider
		GameObject gameobject = other.gameObject;

		// Check if this gameobject has an script Character
		Character character = (Character)gameobject.GetComponent(typeof(Character));

		if (character != null)
		{
			Debug.Log("Character is using stairs");
			character.StairsTransistion();
		}
	}
}
