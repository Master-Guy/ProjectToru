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
		tilemap.SetTile(new Vector3Int(-1, 0, 0), tile);
		RoomBehaviour[] rooms = grid.GetComponentsInChildren<RoomBehaviour>();
		foreach (RoomBehaviour room in rooms)
		{
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
