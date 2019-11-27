using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutePathFinding : MonoBehaviour
{
	[NonSerialized]
	public PathFinding pf;

	[NonSerialized]
	public List<Vector3> path;

	[NonSerialized]
	public Animator animator;

	[NonSerialized]
	public int current = 0;

	public void Awake()
	{
		path = new List<Vector3>();
	}

	public void Start()
	{
		animator = GetComponent<Animator>();

		pf = new PathFinding();
	}

	public void WayPointsWalk()
	{
		if (path.Count > 0)
		{
			if (!GetComponent<Character>().playerOnTheStairs)
			{
				Vector3 newPosition = path[current];

				if (transform.position == newPosition)
				{
					current++;
				}
				transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * 4);
			}

			if (current >= path.Count)
			{
				current = 0;
				path.Clear();
			}
		}
	}

	public Room getCoRoom(Vector2 loc)
	{
		Room r;

		foreach (GameObject v in GameObject.FindGameObjectsWithTag("Room"))
		{
			r = v.GetComponent<Room>();

			if (loc.x >= v.transform.position.x && loc.x < v.transform.position.x + r.GetPosition().x && loc.y > r.GetPosition().y && loc.y < r.GetPosition().y + r.GetSize().y)
			{
				return r;
			}
		}
		return null;
	}

	public void checkDoorClosed(Collider2D other)
	{
		if (other.gameObject.GetComponent<CardReader>())
		{
			if (other.gameObject.GetComponent<CardReader>().getDoor().IsClosed())
			{
				if (gameObject.GetComponent<Character>().HasKey(other.gameObject.GetComponent<CardReader>().GetColor()))
				{
					other.gameObject.GetComponent<CardReader>().getDoor().Open();
				}
				else if (gameObject.GetComponent<NPC>())
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
