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

	[NonSerialized]
	public GameObject targetFurniture;

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
				StopPathFinding();
			}
		}
	}

	public Room getCoRoom(Vector2 loc)
	{
		Room r;

		foreach (GameObject v in GameObject.FindGameObjectsWithTag("Room"))
		{
			r = v.GetComponent<Room>();

			if (loc.x >= r.transform.position.x && loc.x <= (Math.Round(r.transform.position.x) + (int)r.GetSize().x) && loc.y >= r.transform.position.y && loc.y <= (Math.Round(r.transform.position.y) + (int)r.GetSize().y))
			{
				return r;
			}
		}
		return null;
	}

	public void checkDoorClosed(Collider2D other)
	{
		try
		{
			if (other.gameObject.GetComponent<CardReader>())
			{
				if (other.gameObject.GetComponent<CardReader>().getDoor().IsClosed())
				{
					if (gameObject.GetComponent<Character>().HasKey(other.gameObject.GetComponent<CardReader>().GetColor()) || other.gameObject.GetComponent<CardReader>().GetColor().ToString().Equals("Disabled"))
					{
						other.gameObject.GetComponent<CardReader>().getDoor().Open();
					}
					else if (gameObject.GetComponent<NPC>())
					{
						if (gameObject.GetComponent<NPC>().HasKey(other.gameObject.GetComponent<CardReader>().GetColor()) || other.gameObject.GetComponent<CardReader>().GetColor().ToString().Equals("Disabled"))
						{
							other.gameObject.GetComponent<CardReader>().getDoor().Open();
						}
						else
						{
							if (path.Count != 0)
							{
								StopPathFinding();
							}
						}
					}
					else
					{
						if (path.Count != 0)
						{
							StopPathFinding();
						}
					}
				}
			}
		}
		catch (NullReferenceException) { }
	}

	public void StopPathFinding()
	{
		current = 0;
		path.Clear();
		targetFurniture = null;

		Debug.Log("Clear");
	}
}