using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OutsideWallGenerator : MonoBehaviour
{
	[SerializeField]
	BuildingBehaviour building = null;

	[SerializeField]
	Tilemap tilemap = null;

	[SerializeField]
	Tile tile = null;

	// Start is called before the first frame update
	void Start()
	{
		RoomBehaviour[] rooms = building.GetRooms();

		foreach (RoomBehaviour room in rooms)
		{
			// TOP / BOTTOM
			for (int x = -1; x < room.GetSize().x + 1; x++)
			{
				// TOP
				tilemap.SetTile(new Vector3Int(x, room.GetSize().y, 0) + room.GetPosition(), tile);

				//// BOTTOM
				tilemap.SetTile(new Vector3Int(x, -1, 0) + room.GetPosition(), tile);
			}

			// LEFT / RIGHT
			for (int y = 0; y < room.GetSize().y; y++)
			{
				// LEFT
				tilemap.SetTile(new Vector3Int(-1, y, 0) + room.GetPosition(), tile);

				// RIGHT
				tilemap.SetTile(new Vector3Int(room.GetSize().x, y, 0) + room.GetPosition(), tile);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
