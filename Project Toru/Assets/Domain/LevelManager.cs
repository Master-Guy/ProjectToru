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
		tilemapWalls = walls.GetComponent<Tilemap>();
		tilemapBackground = background.GetComponent<Tilemap>();
		GenerateWalls();
		GenerateBackground();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void GenerateWalls()
	{
		foreach (ScriptableRoom r in rooms)
		{
			for (int i = 0; i <= r.width; i++)
			{
				tilemapWalls.SetTile(r.position + new Vector3Int(i, 0, 0), r.theme.bottom);
				tilemapWalls.SetTile(r.position + new Vector3Int(i, r.height, 0), r.theme.top);
			}
			for (int j = 0; j <= r.height; j++)
			{
				tilemapWalls.SetTile(r.position + new Vector3Int(0, j, 0), r.theme.left);
				tilemapWalls.SetTile(r.position + new Vector3Int(r.width, j, 0), r.theme.right);
			}
			tilemapWalls.SetTile(r.position + new Vector3Int(0, 0, 0), r.theme.bottomLeft);
			tilemapWalls.SetTile(r.position + new Vector3Int(r.width, 0, 0), r.theme.bottomRight);
			tilemapWalls.SetTile(r.position + new Vector3Int(0, r.height, 0), r.theme.topLeft);
			tilemapWalls.SetTile(r.position + new Vector3Int(r.width, r.height, 0), r.theme.topRight);
		}
	}

	void GenerateBackground()
	{
		foreach (ScriptableRoom r in rooms)
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


}
