﻿using System;
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

/*			Debug.Log(r.name);
			Debug.Log(loc.x + " - " + loc.y);
			Debug.Log("loc x: " + loc.x + " > " + r.transform.position.x);
			Debug.Log("loc y: " + loc.y + " > " + r.transform.position.y);
			Debug.Log("loc x: " + loc.x + " < " + (Math.Round(r.transform.position.x) + (int)r.GetSize().x));
			Debug.Log("loc y: " + loc.y + " < " + (Math.Round(r.transform.position.y) + (int)r.GetSize().y));
			Debug.Log("-----------------------");

			if (loc.x >= r.transform.position.x && loc.x <= (Math.Round(r.transform.position.x) + (int)r.GetSize().x) && loc.y >= r.transform.position.y && loc.y <= (Math.Round(r.transform.position.y) + (int)r.GetSize().y))
			{
				return r;
			}*/

			if (loc.x > r.transform.position.x && loc.x < (r.transform.position.x + r.GetPosition().x) && Math.Round(loc.y) > (int)(r.GetPosition().y) && Math.Round(loc.y) < (Math.Round(r.transform.position.y) + (int)r.GetSize().y))
			{
				//Debug.Log("Return: " + r.name);
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