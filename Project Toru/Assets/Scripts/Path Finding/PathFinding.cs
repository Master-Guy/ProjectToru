using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
	public Room startRoom;

	private Node startNode;

	private void Start()
	{
		startNode = new Node(null, startRoom);

		Debug.Log("Aantal Nodes: " + Node.node.Count);
	}
}
