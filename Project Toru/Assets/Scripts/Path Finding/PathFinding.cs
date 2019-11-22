using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public Room startRoom, endRoom;

	private List<Node> openList;
	private List<Node> closedList;
	private List<Room> path;

	private double distance;

	private void Awake()
	{
		//Initialize variables
		openList = new List<Node>();
		closedList = new List<Node>();

		path = new List<Room>();
	}

	private void Start()
	{
		//Start the Node Network
		new Node(null, startRoom);

		//Add start Node to openList
		openList.Add(GetNodFromRoom(startRoom));

		//Get Playable character
		Character ch = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

		//Find fastest path
		AStar(GetNodFromRoom(startRoom), GetNodFromRoom(endRoom), ch) ;
	}

	private void AStar(Node startNode, Node endNode, Character ch)
	{
		List<Node> nodes = Node.node;

		Node currentNode = startNode;

		distance = 0.00;

		for(int i = 0; i < 10; i++)
		{
			if(currentNode.canLeft())
			{
				Calculate(currentNode.left, endNode);
			}
			if (currentNode.canRight())
			{
				Calculate(currentNode.right, endNode);
			}
			if (currentNode.canUp())
			{
				Calculate(currentNode.up, endNode);
			}
			if (currentNode.canDown())
			{
				Calculate(currentNode.down, endNode);
			}
		}
	}

	private Node GetNodFromRoom(Room r)
	{
		foreach(Node n in Node.node)
		{
			if(n.GetRoom().Equals(r))
			{
				return n;
			}
		}
		return null;
	}

	private double Calculate(Node next, Node end)
	{
		double currentDistance = Vector3.Distance(next.GetRoom().gameObject.transform.position, end.GetRoom().gameObject.transform.position);
		if(distance > currentDistance)
		{
			distance = currentDistance;
		}
		return distance;
	}

	private bool PlayerHasColor(Node n, Character ch)
	{
		foreach(Key i in ch.inventory.getItemsList())
		{
			if(i.color == n.GetRoom().GetCardReaderLeft().GetColor() || i.color == n.GetRoom().GetCardReaderRight().GetColor())
			{
				return true;
			}
		}
		return false;
	}

	private void ClearCost()
	{
		foreach(Node n in Node.node)
		{
			n.cost = 0;
		}
	}
}