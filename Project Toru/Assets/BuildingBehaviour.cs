using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingBehaviour : MonoBehaviour
{
	[SerializeField]
	Grid grid = null;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	//void Update()
	//{

	//}

	public RoomBehaviour[] GetRooms()
	{
		return grid.GetComponentsInChildren<RoomBehaviour>();
	}

	public int getTotalRooms()
	{
		return GetRooms().Length;
	}

	public Vector2Int size()
	{
		RoomBehaviour[] rooms = GetRooms();

		int left = int.MaxValue;
		int right = int.MinValue;
		int top = int.MinValue;
		int bottom = int.MaxValue;

		foreach (RoomBehaviour room in rooms)
		{

			Vector3Int position = room.GetPosition();
			if (position.y < bottom)
				bottom = position.y;

			if (position.x < left)
				left = position.x;

			if (position.x + room.GetSize().x > right)
				right = position.x + room.GetSize().x;

			if (position.y + room.GetSize().y > top)
				top = position.y + room.GetSize().y;
		}

		return new Vector2Int(right - left, top - bottom);
	}

	//   public Vector2Int center()
	//{

	//}

	//   public Vector2Int bottomLeft()
	//{

	//}

}
