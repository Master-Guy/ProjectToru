using Assets.Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
	public List<ScriptableRoom> rooms;
	public GameObject walls;
	private Tilemap tilemap;

	// Start is called before the first frame update
	void Start()
	{
		tilemap = walls.GetComponent<Tilemap>();
		foreach (ScriptableRoom r in rooms)
		{
			for (int i = 0; i <= r.width; i++)
			{
				tilemap.SetTile(r.position + new Vector3Int(i, 0, 0), r.theme.bottom);
				tilemap.SetTile(r.position + new Vector3Int(i, r.height, 0), r.theme.bottom);
			}
			for (int j = 0; j <= r.height; j++)
			{
				tilemap.SetTile(r.position + new Vector3Int(0, j, 0), r.theme.bottom);
				tilemap.SetTile(r.position + new Vector3Int(r.width, j, 0), r.theme.bottom);
			}

		}
	}

	// Update is called once per frame
	void Update()
	{

	}


}
