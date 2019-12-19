using Assets.Scripts.Behaviour;
using Assets.Scripts.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePathFindingPlayable : ExecutePathFinding
{
	public void Update()
	{
		MousePointInput();
		WayPointsWalk();
	}

	private void MousePointInput()
	{
		if(GetComponent<Character>().Equals(Character.selectedCharacter)) {
			if (Input.GetMouseButtonDown(1))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Plane plane = new Plane(Vector3.forward, transform.position);
				float dist = 0;

				if (plane.Raycast(ray, out dist))
				{
					Vector3 pos = ray.GetPoint(dist);

					Debug.Log(pos.x);
					Debug.Log(pos.y);

					targetFurniture = GetFurniture(pos);

					PathFinding(pos);
				}
			}
		}
	}

	private GameObject GetFurniture(Vector3 pos)
	{
		foreach (GameObject f in GameObject.FindGameObjectsWithTag("Furniture"))
		{
			if(pos.x > f.transform.position.x && pos.x < (f.transform.position.x + f.GetComponent<RectTransform>().rect.width) && pos.y > f.transform.position.y && pos.y < (f.transform.position.y + f.GetComponent<RectTransform>().rect.height))
			{
				Debug.Log("Gevonden !!");
				return f;
			}
		}
		return null;
	}

	public void PathFinding(Vector3 pos)
	{
		Room positionRoom = getCoRoom(pos);

		current = 0;
		path.Clear();

		if (positionRoom != null)
		{
			pos = new Vector2(pos.x, positionRoom.transform.position.y + 1);

			Room characterRoom;

			try
			{
				characterRoom = GetComponent<Character>().currentRoom.GetComponent<Room>();
			}
			catch (UnassignedReferenceException)
			{
				characterRoom = GetEntranceRoom();

				path.Add(new Vector2(characterRoom.transform.position.x - 1, characterRoom.transform.position.x + 1));
			}

			if (!positionRoom.Equals(characterRoom))
			{
				path = pf.CalculateTransforms(positionRoom, characterRoom);
			}
			path.Add(pos);
		}
		else
		{
			Room entranceRoom = GetEntranceRoomToOutside(pos);

			//Code for inside to outside
			try
			{
				if (entranceRoom != null)
				{
					path = pf.CalculateTransforms(entranceRoom, GetComponent<Character>().currentRoom.GetComponent<Room>());
					path.Add(new Vector2(pos.x, entranceRoom.transform.position.y + 1));
				}
			}
			catch (UnassignedReferenceException)
			{
				//Outside to outside code
				path.Add(new Vector2(pos.x, entranceRoom.transform.position.y + 1));
			}
		}
	}

	private Room GetEntranceRoomToOutside(Vector2 pos)
	{
		foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
		{
			Room r = room.GetComponent<Room>();

			if (r.name.StartsWith("Entrance") && pos.y > (r.transform.position.y + 0.1) && pos.y < (r.transform.position.y + 2))
			{
				return r;
			}
		}
		return null;
	}

	private Room GetEntranceRoom()
	{
		foreach(GameObject room in GameObject.FindGameObjectsWithTag("Room"))
		{
			Room r = room.GetComponent<Room>();

			if(r.name.StartsWith("Entrance") && transform.position.y >= room.transform.position.y && transform.position.y <= (transform.position.y + r.GetSize().y))
			{
				return r;
			}
		}
		return null;
	}

	private void CheckFurnitureTarget(Collider2D other)
	{
		if (targetFurniture != null)
		{
			var e = other.gameObject.GetComponent<Assets.Scripts.Options.Event>();
			if (e != null)
			{
				Debug.Log("Pathfinding");
				e.AddActor(GetComponent<Character>());
				CurrentEventWindow.Current.AddEvent(e);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		checkDoorClosed(other);

		CheckFurnitureTarget(other);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		checkDoorClosed(other);
	}

	public bool checkForFurnitureCollider()
	{
		return (path.Count == 0 && targetFurniture == null);
	}
}