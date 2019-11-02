using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour, INPC
{
	private NPC npc;

    private bool handsUp = false;
	private bool flee = false;

	public Employee(NPC npc)
	{
		this.npc = npc;
	}

	public void Idle()
	{
		npc.change = new Vector3(Mathf.PingPong(Time.time, 1.5f) + npc.startingPosition.x, npc.transform.position.y, npc.transform.position.z);
		npc.Move();
	}

	public void Surrender()
	{
		dropBag();

		if (!npc.currentRoom.SelectedPlayerInRoom())
		{
			npc.state = npcState.Flee;
		}

        if (!handsUp)
        {
            HandsUp();
        }
	}

	public void Flee()
	{
		if (!flee)
		{
			Debug.Log("Flee()");
			flee = true;
		}

		if (handsUp)
		{
			handsUp = false;
			npc.animator.SetBool("Surrendering", false);
			npc.animator.SetBool("isMoving", false);
			npc.animator.SetFloat("changeX", 0);
			npc.animator.SetFloat("changeY", 0);
		}
		// Method that requires pathfinding first, npc will search for a nearby exit.
	}

	public void Defend()
	{
		// Method that will implement any fighting back mechanics
	}

	public void CallForHelp()
	{
		// Method that looks for other npcs nearby and calls them for help
	}

	public GameObject GetCurrentRoom()
	{
		throw new NotImplementedException();
	}

	//Drops the items in the bag a NPC have
	private void dropBag()
	{
		if (npc.bag.Count > 0)
		{
			foreach (GameObject b in npc.bag)
			{
				Instantiate(b, new Vector3((npc.transform.position.x + UnityEngine.Random.Range(-1f, 1f)), npc.transform.position.y, npc.transform.position.z), Quaternion.identity);
			}
			npc.bag.Clear();
		}
	}

    private void HandsUp()
    {
        handsUp = true;
        npc.animator.SetBool("Surrendering", true);
        npc.Say("Don't shoot!");
        
        foreach(NPC npc in npc.currentRoom.npcsInRoom)
        {
            npc.state = npcState.Surrender;
        }
    }
}
