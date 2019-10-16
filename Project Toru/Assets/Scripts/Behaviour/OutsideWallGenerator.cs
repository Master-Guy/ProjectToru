using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OutsideWallGenerator : MonoBehaviour
{
	[SerializeField]
	Grid grid = null;

	[SerializeField]
	Tilemap tilemap = null;

	[SerializeField]
	EdgeCollider2D collider = null;

	[SerializeField]
	Tile tile = null;

	// Start is called before the first frame update
	void Start()
	{
		//tilemap.SetTile(new Vector3Int(-1, 0, 0), tile);
		RoomBehaviour[] rooms = grid.GetComponentsInChildren<RoomBehaviour>();

		foreach (RoomBehaviour room in rooms)
		{
			// TOP / BOTTOM
			for (int x = -1; x < room.size.x + 1; x++)
			{
				// TOP
				tilemap.SetTile(new Vector3Int(x, room.size.y, 0) + Vector3Int.FloorToInt(room.gameObject.transform.localPosition), tile);

				//// BOTTOM
				tilemap.SetTile(new Vector3Int(x, -1, 0) + Vector3Int.FloorToInt(room.gameObject.transform.localPosition), tile);
			}

			// LEFT / RIGHT
			for (int y = 0; y < room.size.y; y++)
			{
				// LEFT
				tilemap.SetTile(new Vector3Int(-1, y, 0) + Vector3Int.FloorToInt(room.gameObject.transform.localPosition), tile);

				// RIGHT
				tilemap.SetTile(new Vector3Int(room.size.x, y, 0) + Vector3Int.FloorToInt(room.gameObject.transform.localPosition), tile);
			}

			Debug.Log(room.size);
			//Debug.Log(room.gameObject.transform.localPosition.x);
			//Debug.Log(room.gameObject.transform.localPosition.y);
			//Debug.Log(room.gameObject.transform.localPosition.z);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
