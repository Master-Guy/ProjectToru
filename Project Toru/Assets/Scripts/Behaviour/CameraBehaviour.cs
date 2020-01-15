using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public Transform target = null;

	public float smoothing;
	public float distance;
	public float minDistance = 6;
	public float maxDistance = 12;

	public static bool freeLook;

	private Vector3 change;

	void Update()
	{
		Move();
		Zoom();

		if (!freeLook && target == null)
		{
			if (Character.selectedCharacter != null)
			{
				target = Character.selectedCharacter.transform;
			}
		}
	}

	void LateUpdate()
	{
		if (target != null)
		{
			if (transform.position != target.position)
			{
				Vector3 targetVector = new Vector3(target.position.x, target.position.y, transform.position.z);
				transform.position = Vector3.Lerp(transform.position, targetVector, smoothing);
			}
		}
	}

	void Move()
	{
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

		if (change != Vector3.zero)
		{
			if (!freeLook)
			{
				freeLook = true;
			}
			if (target != null)
			{
				target = null;
			}
			transform.position += change * Time.deltaTime * 15;
		}
	}

	void Zoom()
	{
		distance -= Input.mouseScrollDelta.y * Time.deltaTime * 30;
		distance = Mathf.Clamp(distance, minDistance, maxDistance);
		GetComponent<Camera>().orthographicSize = distance;
	}
}
