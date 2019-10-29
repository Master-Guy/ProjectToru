using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{

	// Note: The 
	[SerializeField]
	public Theme theme = null;

	[SerializeField]
	Tilemap walls = null;

	[SerializeField]
	Tilemap background = null;

	[SerializeField]
	bool lightsOn;

	[SerializeField]
	WallController wallController;

	public RoomBehaviour LeftRoom;
	public RoomBehaviour RightRoom;

	[SerializeField]
	Vector2Int size = new Vector2Int(0, 0);

	void Start()
	{
		if (size.x == 0 || size.y == 0)
		{
			Debug.LogError("A room size must be set manualy");
		}
		GenerateBackground();

		if (LeftRoom != null)
		{
			wallController.EnableLeftWall(false);
		}

		if (RightRoom != null)
		{
			wallController.EnableRightWall(false);
		}
	}

	private void GenerateBackground()
	{
		if (lightsOn && background.size.x == 0)
		{
			for (int i = 0; i < walls.size.x; i++)
			{
				for (int j = 1; j < walls.size.y; j++)
				{
					background.SetTile(new Vector3Int(i, j, 0), theme.center);
				}
			}
		}

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log("Right Mouse Button Clicked on: " + name);
		}
	}

	/// <summary>
	/// This function returns the current position seen from bottom left
	/// </summary>
	/// <returns>Returns current position from bottom left</returns>
	public Vector3Int GetPosition()
	{
		return Vector3Int.FloorToInt(this.gameObject.transform.localPosition);
	}

	/// <summary>
	/// This function returns the size of the room set by level designer
	/// </summary>
	/// <returns>Returns current size of room</returns>
	public Vector2Int GetSize()
	{
		return size;
	}
}