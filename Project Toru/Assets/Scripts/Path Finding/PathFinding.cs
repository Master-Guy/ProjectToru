using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public Room startRoom, endRoom;

	private void Start()
	{
		Node.endRoom = endRoom;

		new Node(null, startRoom);

		Loop(Node.endNode);
	}

	private void Loop(Node n)
	{
		Node current = n;

		Debug.Log(current.GetRoom().name);

		while (current.parent != null)
		{
			current = current.parent;
			Debug.Log(current.GetRoom().name);
		}
	}
}