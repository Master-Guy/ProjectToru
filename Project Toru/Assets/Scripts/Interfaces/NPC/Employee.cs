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
		npc.change = new Vector3(Mathf.PingPong(Time.time * 0.8f, 1.5f) + 15, npc.transform.position.y, npc.transform.position.z);
	}

	public void Surrender()
	{
		npc.animator.SetBool("Surrendering", true);
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
}
