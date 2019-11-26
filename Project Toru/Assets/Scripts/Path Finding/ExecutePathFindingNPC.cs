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
		path = pf.CalculateTransforms(getCoRoom(pos), GetComponent<NPC>().getRoom());
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		checkDoorClosed(other);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		checkDoorClosed(other);
	}

	private void checkDoorClosed(Collider2D other)
	{
		if (other.gameObject.GetComponent<CardReader>())
		{
			if (other.gameObject.GetComponent<CardReader>().getDoor().IsClosed())
			{
				if (gameObject.GetComponent<NPC>().HasKey(other.gameObject.GetComponent<CardReader>().GetColor()))
				{
					other.gameObject.GetComponent<CardReader>().getDoor().Open();
				}
				else
				{
					if (path.Count != 0)
					{
						path.Clear();
					}
				}
			}
		}
	}
}
