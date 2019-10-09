using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class RoomBehaviour : MonoBehaviour
{

	// Note: The 
	[SerializeField]
	public RoomTheme theme = null;

	[SerializeField]
	private Tilemap walls = null;

	[SerializeField]
	private Tilemap background = null;

	[SerializeField]
	private bool lightsOn;

	void Start()
	{
		GenerateBackground();
	}

	void Update()
	{

	}

	private void GenerateBackground()
	{
		if (lightsOn && background.size.x == 0)
		{
			for (int i = 0; i < walls.size.x; i++)
			{
				for (int j = 1; j < walls.size.y; j++)
				{
					Debug.Log(i + " - " + j);
					background.SetTile(new Vector3Int(i, j, 0), theme.center);
				}
			}
		}
	}
}