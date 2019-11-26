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

		for (int i = 0; i < route.Count; i++)
		{

			List<Vector3> localTransforms = new List<Vector3>();

			if (i == route.Count - 1)
			{
				//Do Nothing
			}
			else if (route[i].nodeRoom.isRoom() && route[i].parent.nodeRoom.isRoom())
			{
				//Normal room to normalRoom
				localTransforms = getRoomTransform(route[i], null);
			}
			else if(route[i].nodeRoom.isRoom() && !route[i].parent.nodeRoom.isRoom())
			{
				//Stair to normalRoom
				localTransforms = getRoomTransform(route[i], null);
			}
			else if(!route[i].nodeRoom.isRoom() && !route[i].parent.nodeRoom.isRoom())
			{
				//Stair to Stair
				localTransforms = getStairTransform(route[i], null);
			}

			path.AddRange(localTransforms);
		}
		return path;
	}

	private List<Node> CalculateRoute(Room startRoom, Room endRoom)
	{
		Node.vissited.Clear();
		Node.node.Clear();

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

	private List<Vector3> getRoomTransform(Node n, Node l)
	{
		List<Vector3> local = new List<Vector3>();

		if (l == null)
		{
			if (n.nodeRoom.GetPosition().x < n.parent.nodeRoom.GetPosition().x)
			{
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 1, n.nodeRoom.GetPosition().y + 1));
			}
			else
			{
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + n.nodeRoom.GetSize().x - 1, n.nodeRoom.GetPosition().y + 1));
			}
		}
		else
		{
			if (n.nodeRoom.GetPosition().x > l.nodeRoom.GetPosition().x)
			{
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 1, n.nodeRoom.GetPosition().y + 1));
			}
			else
			{
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + n.nodeRoom.GetSize().x - 1, n.nodeRoom.GetPosition().y +  + 1));
			}
		}
		return local;
	}

	private List<Vector3> getStairTransform(Node n, Node l)
	{
		List<Vector3> local = new List<Vector3>();

		if (l == null)
		{
			if (n.nodeRoom.GetPosition().y < n.parent.nodeRoom.GetPosition().y)
			{
				//Go up
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 5, n.nodeRoom.GetPosition().y + 1));
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 5, n.nodeRoom.GetPosition().y + 2));
			}
			else
			{
				//Go down
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 2, n.nodeRoom.GetPosition().y + 1));
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 2, n.nodeRoom.GetPosition().y + 2));
			}
		}
		else
		{
			if (n.nodeRoom.GetPosition().y > l.nodeRoom.GetPosition().y)
			{
				//Go up
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 5, l.nodeRoom.GetPosition().y + 1));
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 5, l.nodeRoom.GetPosition().y + 2));
			}
			else
			{
				//Go down
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 2, l.nodeRoom.GetPosition().y + 1));
				local.Add(new Vector3(n.nodeRoom.GetPosition().x + 2, l.nodeRoom.GetPosition().y + 2));
			}
		}
		return local;
	}
}