using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	private Node parent;

	private Room nodeRoom;

	private Node leftNode, rightNode, upStairs;

	public static List<Room> vissited = new List<Room>();
	public static List<Node> node = new List<Node>();

	public Node(Node parent, Room NodeRoom)
	{
		this.parent = parent;
		this.nodeRoom = NodeRoom;

		vissited.Add(this.nodeRoom);
		node.Add(this);

		Debug.Log("Room: " + nodeRoom.name);

		goLeft();
		goRight();
		goUpStairs();
	}

	private bool goLeft()
	{
		if (nodeRoom.LeftRoom == null || vissited.Contains(nodeRoom.LeftRoom))
		{
			return true;
		}
		leftNode = new Node(this, nodeRoom.LeftRoom);
		return false;
	}

	private bool goRight()
	{
		if (nodeRoom.RightRoom == null || vissited.Contains(nodeRoom.RightRoom))
		{
			return true;
		}
		rightNode = new Node(this, nodeRoom.RightRoom);
		return false;
	}

	private bool goUpStairs()
	{
		if(nodeRoom.GetComponent<StairsBehaviour>() != null)
		{
			if (nodeRoom.GetComponent<StairsBehaviour>().Upstair != null)
			{
				upStairs = new Node(this, nodeRoom.GetComponent<StairsBehaviour>().Upstair.GetComponent<Room>());
				return false;
			}
		}
		return true;
	}
}