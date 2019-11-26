using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	PathFinding pf;

	List<Vector3> path;

	Animator animator;

	public int current = 0;

	public void Awake()
	{
		path = new List<Vector3>();
	}

	public void Start()
	{
		animator = GetComponent<Animator>();

		pf = new PathFinding();
	}

	public void Update()
	{
		MousePointInput();
		WayPointsWalk();
	}

	private void MousePointInput()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane plane = new Plane(Vector3.forward, transform.position);
			float dist = 0;
			if (plane.Raycast(ray, out dist))
			{
				Vector3 pos = ray.GetPoint(dist);
				Room positionRoom = getCoRoom(pos);

				if (positionRoom != null)
				{
					current = 0;
					path.Clear();
					pos = new Vector2(pos.x, positionRoom.transform.position.y + 1);

					Room characterRoom = GetComponent<Character>().currentRoom.GetComponent<Room>();

					if(!positionRoom.Equals(characterRoom))
					{
						path = pf.CalculateTransforms(positionRoom, characterRoom);
					}
					path.Add(pos);
				}
			}
		}
	}

	private void WayPointsWalk()
	{
		if (path.Count > 0)
		{
			if (!GetComponent<Character>().playerOnTheStairs)
			{
				Vector3 newPosition = path[current];

				if (transform.position == newPosition)
				{
					current++;
				}
				transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * 4);
			}

			if (current >= path.Count)
			{
				current = 0;
				path.Clear();
			}
		}
	}

	private Room getCoRoom(Vector2 loc)
	{
		Room r;

		foreach(GameObject v in GameObject.FindGameObjectsWithTag("Room"))
		{
			r = v.GetComponent<Room>();

			if (loc.x >= v.transform.position.x && loc.x < v.transform.position.x + r.GetPosition().x && loc.y > r.GetPosition().y && loc.y < r.GetPosition().y + r.GetSize().y)
			{
				return r;
			} 
		}
		return null;
	}
}