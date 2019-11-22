using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public Room startRoom, endRoom;

	private List<Node> openList;
	private List<Node> closedList;
	private List<Room> path;

	private void Awake()
	{
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
}
