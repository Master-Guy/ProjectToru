using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	private Node parent;

	private Room nodeRoom;

	private Node leftNode, rightNode, upStairs, downStairs;

	public static List<Room> vissited = new List<Room>();
	public static List<Node> node = new List<Node>();

	public Node(Node parent, Room NodeRoom)
	{
		this.parent = parent;
		this.nodeRoom = NodeRoom;

		

		vissited.Add(this.nodeRoom);
		node.Add(this);

		Debug.Log("Room: " + nodeRoom.name);

		GoLeft();
		GoRight();
		GoUpStairs();
		GoDownStairs();
	}

	private bool GoLeft()
	{
		if (nodeRoom.LeftRoom == null || vissited.Contains(nodeRoom.LeftRoom))
		{
			return true;
		}
		leftNode = new Node(this, nodeRoom.LeftRoom);
		return false;
	}

	private bool GoRight()
	{
		if (nodeRoom.RightRoom == null || vissited.Contains(nodeRoom.RightRoom))
		{
			return true;
		}
		rightNode = new Node(this, nodeRoom.RightRoom);
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
				upStairs = new Node(this, nodeRoom.GetComponent<StairsBehaviour>().Upstair.GetComponent<Room>());
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
				downStairs = new Node(this, nodeRoom.GetComponent<StairsBehaviour>().Downstairs.GetComponent<Room>());
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