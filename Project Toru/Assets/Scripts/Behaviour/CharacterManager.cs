using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
	private static CharacterManager cm;

	private static GameObject[] characters;

	void Start()
	{
		if (characters == null)
		{
			characters = GameObject.FindGameObjectsWithTag("Player");
		}
	}

	public void disableCharacterMovement()
	{
		foreach (GameObject c in characters)
		{
			Character character = (Character)c.GetComponent(typeof(Character));
			character.disableMovement();
		}
	}
}
