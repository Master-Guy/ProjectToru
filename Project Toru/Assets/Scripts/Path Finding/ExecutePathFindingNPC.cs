using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePathFindingNPC : ExecutePathFinding
{
	public Vector3 targetVector;
	public bool test = true;

	private void Update()
	{
		WayPointsWalk();

		if(test)
		{
			PathFinding(targetVector);
			test = false;
		}
	}

	public void setPosTarget(Vector3 pos)
	{
		PathFinding(pos);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		checkDoorClosed(other);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		checkDoorClosed(other);
	}
}
