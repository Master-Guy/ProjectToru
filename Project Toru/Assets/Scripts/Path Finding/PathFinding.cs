using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
	public Room startRoom, endRoom;

	public List<Node> path;

	public List<Vector3> CalculateTransforms(Room startRoom, Room endRoom)
	{
		List<Vector3> path = new List<Vector3>();

		List<Node> route = CalculateRoute(startRoom, endRoom);

		//route.Reverse();

		foreach(Node n in route)
		{
			Debug.Log(n.nodeRoom.name);
		}

		for (int i = 0; i < route.Count; i++)
		{
			List<Vector3> localTransforms = new List<Vector3>();

			//Normal room
			if (route[i].nodeRoom.getStairScript() == null)
			{
				localTransforms = getRoomTransform(route[i]);
			}
			else
			{
				if (route[i].nodeRoom.getStairScript().Upstair != null)
				{
					//stairs
					localTransforms = getStairTransform(route[i]);
				}
			}

			path.AddRange(localTransforms);
		}

		return path;
	}

	private List<Node> CalculateRoute(Room startRoom, Room endRoom)
	{
		Node.endRoom = endRoom;

		new Node(null, startRoom);

		return GetPathToList(Node.endNode);
	}

	private List<Node> GetPathToList(Node n)
	{
		Node current = n;

		path = new List<Node>();

		path.Add(n);

		while (current.parent != null)
		{
			current = current.parent;
			path.Add(current);
		}
		return path;
	}

	private List<Vector3> getRoomTransform(Node n)
	{
		List<Vector3> local = new List<Vector3>();
		if (n.parent != null)
		{
			//Go left
			if (n.nodeRoom.transform.position.x > n.parent.nodeRoom.transform.position.x)
			{
				local.Add(new Vector3(n.nodeRoom.transform.position.x, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
				return local;
			}
			//Go Right
			local.Add(new Vector3(n.nodeRoom.transform.position.x + n.nodeRoom.GetSize().x, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
			return local;
		}
		return local;
	}

	private List<Vector3> getStairTransform(Node n)
	{
		List<Vector3> local = new List<Vector3>();

		if (n.parent != null)
		{
			//Go down
			if (n.nodeRoom.transform.position.y > n.parent.nodeRoom.transform.position.y)
			{
				local.Add(new Vector3(n.nodeRoom.transform.position.x + 2, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
				local.Add(new Vector3(n.nodeRoom.transform.position.x + 2, n.nodeRoom.transform.position.y + 2, n.nodeRoom.transform.position.z));
				return local;
			}
			//Go Up
			local.Add(new Vector3(n.nodeRoom.transform.position.x + 5, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
			local.Add(new Vector3(n.nodeRoom.transform.position.x + 5, n.nodeRoom.transform.position.y + 1.9f, n.nodeRoom.transform.position.z));
			return local;
		}
		return local;
	}
}