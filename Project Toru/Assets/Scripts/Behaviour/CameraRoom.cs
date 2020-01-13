﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoom : MonoBehaviour
{
	Room roomObj;

	GameObject[] cameras;

	void Awake()
	{
		roomObj = this.gameObject.GetComponent<Room>();
		cameras = GameObject.FindGameObjectsWithTag("Camera");
	}

	public void AlertGuard()
	{
		if (roomObj.getNPCsInRoom().Count > 0)
		{
			foreach (GameObject npc in roomObj.getNPCsInRoom())
			{
				npc.GetComponent<NPC>().Say("I am warned!");
			}
		}

		Invoke("AlertCops", 18);
	}
}
