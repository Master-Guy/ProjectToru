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
		dropBag();
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
}
