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
					Room positionRoom = getCoRoom(pos);

					if (positionRoom != null)
					{
						current = 0;
						path.Clear();
						pos = new Vector2(pos.x, positionRoom.transform.position.y + 1);

						Room characterRoom;

						try
						{
							characterRoom = GetComponent<Character>().currentRoom.GetComponent<Room>();
						}
						catch (UnassignedReferenceException e)
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
				}
			}
		}
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		checkDoorClosed(other);
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		checkDoorClosed(other);
	}
}