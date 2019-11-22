using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public Room startRoom, endRoom;

	public List<Node> path;

	private void Start()
	{
		//TestScript
		CalculateRoute(startRoom, endRoom);
	}

	public List<Node> CalculateRoute(Room startRoom, Room endRoom)
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

		Debug.Log("Path: " + n.nodeRoom.name);

		while (current.parent != null)
		{
			current = current.parent;
			path.Add(current);

			Debug.Log("Path: " + current.nodeRoom.name);
		}

		return path;
	}
}