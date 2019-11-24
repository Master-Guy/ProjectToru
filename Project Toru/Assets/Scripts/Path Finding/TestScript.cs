using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public Room startRoom, endRoom;

	PathFinding pf;

	List<Vector3> path;

	Animator animator;

	Vector3 change;

	public int current = 0;

	public void Start()
	{
		animator = GetComponent<Animator>();

		pf = new PathFinding();

		path = pf.CalculateTransforms(endRoom, startRoom);

		foreach (Vector3 v in path)
		{
			Debug.Log(v.x + " - " + v.y);
		}
	}

	public void Update()
	{
		if (!GetComponent<Character>().playerOnTheStairs)
		{
			Vector3 newPosition = path[current];

			if(path.Count < current)
			{
				current = path.Count;
			}

			if(transform.position == newPosition)
			{
				current++;
			}
			transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * 4);
		}
	}
}