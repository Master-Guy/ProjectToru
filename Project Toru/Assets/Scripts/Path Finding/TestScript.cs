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

	int current = 0;

	public void Start()
	{
		animator = GetComponent<Animator>();

		pf = new PathFinding();

		path = pf.CalculateTransforms(endRoom, startRoom);

		foreach(Vector3 v in path)
		{
			Debug.Log(v.x + " - " + v.y);
		}
	}

	public void Update()
	{
		if (!GetComponent<Character>().playerOnTheStairs)
		{
			Vector3 newPosition = path[current];

			if ((transform.position - path[current]).sqrMagnitude < 1 * 1)
			{
				if (current < path.Count - 1)
				{
					current++;
				}
			}
			transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * 4);
		}
	}
}