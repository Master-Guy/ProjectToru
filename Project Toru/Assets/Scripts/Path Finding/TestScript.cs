using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public Room startRoom, endRoom;

	PathFinding pf;

	List<Node> path;

	Animator animator;

	Vector3 change;

	int current = 0;
	public float speed;

	public void Start()
	{
		animator = GetComponent<Animator>();

		pf = new PathFinding();

		path = pf.CalculateRoute(endRoom, startRoom);
	}

	public void Update()
	{
		Vector3 newPosition = new Vector3(path[current].nodeRoom.transform.position.x + 5, path[current].nodeRoom.transform.position.y + 1, path[current].nodeRoom.transform.position.z);

		if (gameObject.transform.position == newPosition)
		{
			if (current < path.Count - 1)
			{
				current++;
			}
		}

		transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);

		change = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
		change.Normalize();
		if (change != Vector3.zero)
		{
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		}
		else
		{
			animator.SetBool("moving", false);
		}
	}


}
