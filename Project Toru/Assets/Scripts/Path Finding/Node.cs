using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Node parent;

	public static List<Room> vissited = new List<Room>();
	public static List<Node> node = new List<Node>();
	public static Room endRoom;
	public static Node endNode;

	public Room nodeRoom;

	Node left, right, up, down;

	public Node(Node parent, Room NodeRoom)
	{
		this.parent = parent;
		this.nodeRoom = NodeRoom;

		vissited.Add(this.nodeRoom);
		node.Add(this);

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

	public Node getChild(Node n)
	{
		if (left.Equals(n)) { return left;  }
		if (right.Equals(n)) { return right; }
		if (up.Equals(n)) { return up; }
		if (down.Equals(n)) { return down; }
		return null;
	}
}