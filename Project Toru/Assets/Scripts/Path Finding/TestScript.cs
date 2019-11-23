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
	public float speed;

	public void Start()
	{
		animator = GetComponent<Animator>();

		pf = new PathFinding();

		path = pf.CalculateTransforms(endRoom, startRoom);

		Debug.Log("Path size: " + path.Count);
	}

	public void Update()
	{
		Vector3 newPosition = path[current];

		if (gameObject.transform.position == newPosition)
		{
			current++;
		}
		transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
	}
}