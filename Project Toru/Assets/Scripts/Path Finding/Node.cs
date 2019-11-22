using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Node parent;

	private Room nodeRoom;

	public Node left, right, up, down;

	public static List<Room> vissited = new List<Room>();
	public static List<Node> node = new List<Node>();
	public static Room endRoom;
	public static Node endNode;

	public Node(Node parent, Room NodeRoom)
	{
		this.parent = parent;
		this.nodeRoom = NodeRoom;

		vissited.Add(this.nodeRoom);
		node.Add(this);

		//Debug.Log("Room: " + nodeRoom.name);

		if (nodeRoom.name.Equals(endRoom.name))
		{
			endNode = this;
		}
		else
		{
			GoLeft();
			GoRight();
			GoUpStairs();
			GoDownStairs();
		}
	}

	private bool GoLeft()
	{
		if (nodeRoom.LeftRoom == null || vissited.Contains(nodeRoom.LeftRoom))
		{
			return true;
		}
		left = new Node(this, nodeRoom.LeftRoom);
		return false;
	}

	private bool GoRight()
	{
		if (nodeRoom.RightRoom == null || vissited.Contains(nodeRoom.RightRoom))
		{
			return true;
		}
		right = new Node(this, nodeRoom.RightRoom);
		return false;
	}

	private bool GoUpStairs()
	{
		if(nodeRoom.GetComponent<StairsBehaviour>() != null)
		{
			if (nodeRoom.GetComponent<StairsBehaviour>().Upstair != null)
			{
				if (vissited.Contains(nodeRoom.GetComponent<StairsBehaviour>().Upstair.GetComponent<Room>())) {
					return true;
				}
				up = new Node(this, nodeRoom.GetComponent<StairsBehaviour>().Upstair.GetComponent<Room>());
				return false;
			}
		}
		return true;
	}

	private bool GoDownStairs()
	{ 
		if (nodeRoom.GetComponent<StairsBehaviour>() != null)
		{
			if (nodeRoom.GetComponent<StairsBehaviour>().Downstairs != null)
			{
				if (vissited.Contains(nodeRoom.GetComponent<StairsBehaviour>().Downstairs.GetComponent<Room>()))
				{
					return true;
				}
				down = new Node(this, nodeRoom.GetComponent<StairsBehaviour>().Downstairs.GetComponent<Room>());
				return false;
			}
		}
		return true;
	}

	public Room GetRoom()
	{
		return nodeRoom;
	}
}