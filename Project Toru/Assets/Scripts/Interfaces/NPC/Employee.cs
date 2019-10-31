using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour, INPC
{
	private NPC npc;

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
		npc.animator.SetBool("Surrendering", true);
		npc.Say("Don't shoot");
	}

	public void Flee()
	{
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
}
