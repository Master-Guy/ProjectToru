using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class RoomBehaviour : MonoBehaviour
{

	// Note: The 
	[SerializeField]
	public Room room = null;

	[SerializeField]
	private Tilemap walls = null;

	[SerializeField]
	private Tilemap background = null;

	//public RoomBehaviour(string sprite, RoomBehaviour info)
	//{
	//	//this.info = info;
	//}

	void Start()
	{
		if (room == null || walls == null || background == null)
		{
			Debug.LogError("Error: It is required to assign the Tilemap and room to prefab");
		}

		Debug.Log(room.name);
		//Debug.Log(tiles.size.x);
		//Debug.Log(tiles.size.y);
		//Debug.Log(tiles.size.z);

		GenerateBackground();
	}

	void Update()
	{

	}

	private void GenerateBackground()
	{
		if (room.lightsOn && background.size.x == 0)
		{
			for (int i = 0; i < walls.size.x; i++)
			{
				for (int j = 0; j < walls.size.y; j++)
				{
					Debug.Log(i + " - " + j);
					background.SetTile(new Vector3Int(i, j, 0), room.theme.center);
				}
			}
		}
	}
}