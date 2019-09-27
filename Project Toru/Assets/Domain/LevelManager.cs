using Assets.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
	public List<ScriptableRoom> rooms;
	public GameObject walls;
	public GameObject background;
	private Tilemap tilemapWalls;
	private Tilemap tilemapBackground;

	// Start is called before the first frame update
	void Start()
	{
		GenerateRooms();
	}

	// foreach loop that loops through every room and generates the walls and background
	private void GenerateRooms()
	{
		tilemapWalls = walls.GetComponent<Tilemap>();
		tilemapBackground = background.GetComponent<Tilemap>();

		foreach (ScriptableRoom r in rooms)
		{
			GenerateWalls(r);
			GenerateBackground(r);
		}
	}

	// Generates walls for each room
	private void GenerateWalls(ScriptableRoom r)
	{
		// Sets walls for top and bottom
		for (int i = 0; i <= r.width; i++)
		{
			tilemapWalls.SetTile(r.position + new Vector3Int(i, 0, 0), r.theme.bottom);
			tilemapWalls.SetTile(r.position + new Vector3Int(i, r.height, 0), r.theme.top);
		}

		// Sets walls for both sides
		for (int j = 0; j <= r.height; j++)
		{
			tilemapWalls.SetTile(r.position + new Vector3Int(0, j, 0), r.theme.left);
			tilemapWalls.SetTile(r.position + new Vector3Int(r.width, j, 0), r.theme.right);
		}

		// Sets wall corner pieces
		tilemapWalls.SetTile(r.position + new Vector3Int(0, 0, 0), r.theme.bottomLeft);
		tilemapWalls.SetTile(r.position + new Vector3Int(r.width, 0, 0), r.theme.bottomRight);
		tilemapWalls.SetTile(r.position + new Vector3Int(0, r.height, 0), r.theme.topLeft);
		tilemapWalls.SetTile(r.position + new Vector3Int(r.width, r.height, 0), r.theme.topRight);
	}

	// fills the background of each room with it's assigned theme tile
	private void GenerateBackground(ScriptableRoom r)
	{
		if (r.lightsOn)
		{
			for (int i = 0; i <= r.width; i++)
			{
				for (int j = 0; j <= r.height; j++)
				{
					tilemapBackground.SetTile(r.position + new Vector3Int(i, j, 0), r.theme.center);
				}
			}
		}
	}

}
