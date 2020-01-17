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
				Plane plane = new Plane(Vector3.forward, Character.selectedCharacter.transform.position);
				float dist = 10;

				if (plane.Raycast(ray, out dist))
				{
					Vector2 pos = ray.GetPoint(dist);

					targetFurniture = null;

					PathFinding(pos);
				}
			}
		}
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