using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
	private static CameraRoom  cameraRoom = new CameraRoom();

	bool isDisabled;

	private void Awake()
	{
		isDisabled = false;
	}

	public void DisableCamera()
	{
		isDisabled = true;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(!isDisabled && other.isTrigger && other.tag.Equals("Player"))
		{
			Debug.Log("You have been seen");
			cameraRoom.AlertGuard();
		}
	}
}
