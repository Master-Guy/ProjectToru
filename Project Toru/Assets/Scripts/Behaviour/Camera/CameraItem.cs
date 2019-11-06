using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
	bool isEnabled = true;

	[SerializeField]
	private CameraRoom securityRoom;

	public void DisableCamera()
	{
		isEnabled = false;
	}

	public void EnableCamera()
	{
		isEnabled = true;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (isEnabled && other.isTrigger && other.tag.Equals("Player"))
		{
			securityRoom.AlertGuard();
		}
	}
}
