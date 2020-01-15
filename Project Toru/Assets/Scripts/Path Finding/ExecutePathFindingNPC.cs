using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePathFindingNPC : ExecutePathFinding
{
	private void Update()
	{
		WayPointsWalk();
	}

	public void setPosTarget(Vector2 pos)
	{
		path = pf.CalculateTransforms(getCoRoom(pos), GetComponent<NPC>().currentRoom);
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
