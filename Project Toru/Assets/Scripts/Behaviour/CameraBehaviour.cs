using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	public Transform target = null;
	public float smoothing;

	// Update is called once per frame
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
}
