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
		npc.transform.position = new Vector2(Mathf.PingPong(Time.time * 0.5f, 1.5f) + 15, npc.transform.position.y);
	}

	public void Surrender()
	{
		Debug.Log("OK ok , ill surrender..");
	}

	public void Flee()
	{
		throw new NotImplementedException();
	}

	public void Defend()
	{
		throw new NotImplementedException();
	}

	public void CallForHelp()
	{
		throw new NotImplementedException();
	}

	public GameObject GetCurrentRoom()
	{
		throw new NotImplementedException();
	}
}
