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

		route.Reverse();

		for (int i = 0; i < route.Count; i++)
		{
			List<Vector3> localTransforms = new List<Vector3>();

			if(route[i].parent != null)
			{
				//Normal room
				if(route[i].parent.nodeRoom.getStairScript() == null)
				{
					localTransforms = getRoomTransform(route[i].parent);
				}
				if(route[i].nodeRoom.getStairScript() != null && route[i].parent.nodeRoom.getStairScript() != null)
				{
					localTransforms = getStairTransform(route[i]);
				}
				
			}

			path.AddRange(localTransforms);
		}

		path.Reverse();
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
			//Go left in reverse
			if (n.nodeRoom.transform.position.x < n.parent.nodeRoom.transform.position.x)
			{
				local.Add(new Vector3(n.parent.nodeRoom.transform.position.x + .5f, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
			}
			else
			{
				//Go Right in reverse
				local.Add(new Vector3(n.parent.nodeRoom.transform.position.x + n.parent.nodeRoom.GetSize().x - 0.5f, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
			}
		}
		return local;
	}

	private List<Vector3> getStairTransform2(Node n)
	{
		List<Vector3> local = new List<Vector3>();

		if(n.nodeRoom.transform.position.y < n.nodeRoom.transform.position.y)
		{

		}

		return local;
	}

	private List<Vector3> getStairTransform(Node n)
	{
		List<Vector3> local = new List<Vector3>();

		if (n.parent != null)
		{
			//Go UP
			if (n.nodeRoom.transform.position.y < n.parent.nodeRoom.transform.position.y)
			{
				Debug.Log("Go Up");
				local.Add(new Vector3(n.parent.nodeRoom.transform.position.x + 5, n.parent.nodeRoom.transform.position.y + 2f, n.nodeRoom.transform.position.z));
				local.Add(new Vector3(n.parent.nodeRoom.transform.position.x + 5, n.parent.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
			}
			else
			{
				//Go Down
				Debug.Log("Go down");
				local.Add(new Vector3(n.parent.nodeRoom.transform.position.x + 2, n.nodeRoom.transform.position.y + 2, n.nodeRoom.transform.position.z));
				local.Add(new Vector3(n.parent.nodeRoom.transform.position.x + 2, n.nodeRoom.transform.position.y + 1, n.nodeRoom.transform.position.z));
			}
		}
		return local;
	}
}