using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private npcState currentState = npcState.defaultAction;
	private GameObject currentRoom;

	private bool surrendering = false;
	private bool fleeing = false;

	private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();
		AdjustOrderLayer();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case npcState.defaultAction:
				DefaultAction();
                break;
            case npcState.surrendering:
				Surrender();
                break;
            case npcState.fleeing:
				Flee();
                break;
            default:
                break;
        }
    }

	private void DefaultAction()
	{
		// default movements/actions of npc
	}

	private void Surrender()
	{
		fleeing = false;
		surrendering = true;
		animator.SetBool("Surrendering", true);
	}

	private void Flee()
	{
		surrendering = false;
		fleeing = true;

		// need pathfinding for this part, npc will run towards an exit
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Room"))
		{
			currentRoom = other.gameObject;
		}
	}

	void OnMouseDown()
	{
		// checks if the npc and the selected character are in the same room
		if (currentRoom.GetComponent<Room>().charactersInRoom.Contains(Character.selectedCharacter))
		{
			// going to be more complex, on-screen options on what action to execute.
			currentState = npcState.surrendering;
		}
	}

	// this method makes sure the order layer is correct, creates a fake 3d perspective when character/npcs walk past each other.
	void AdjustOrderLayer()
	{
		GetComponent<SpriteRenderer>().sortingOrder = (int)(-transform.position.y * 1000);
	}
}

public enum npcState
{
    defaultAction,
    surrendering,
    fleeing
}
