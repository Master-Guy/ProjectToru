using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System;

public class Room : MonoBehaviour, IPointerClickHandler
{
	//[SerializeField]
	//Tilemap walls = null;

	//[SerializeField]
	//Tilemap background = null;

	//[SerializeField]
	//bool lightsOn = true;

	[SerializeField]
	WallController wallController = null;

	[SerializeField]
	CardReader cardReaderLeft = null;

	[SerializeField]
	CardReader cardReaderRight = null;

	public Room LeftRoom = null;
	public Room RightRoom = null;

	public Door door = null;

	public HashSet<GameObject> charactersInRoom;
	public HashSet<GameObject> npcsInRoom;

	[SerializeField]
	Vector2Int size = new Vector2Int(0, 0);

	private bool roomHasCamera = false;
	CameraRoom cameraRoom;

	public Room()
	{
		charactersInRoom = new HashSet<GameObject>();
		npcsInRoom = new HashSet<GameObject>();
	}

	void Start()
	{
		// Check if roomsize is set
		if (size.x == 0 || size.y == 0)
		{
			Debug.LogError("A room size must be set manualy");
		}

		// Enable Collider for leftwall
		if (LeftRoom != null)
		{
			wallController?.EnableLeftWall(false);
		}

		// Hide CardReaders
		if (LeftRoom == null)
		{
			cardReaderLeft?.Hide();
		}
		if (RightRoom == null)
		{
			cardReaderRight?.Hide();
		}

		// Tell Cardreaders which door is his door
		cardReaderLeft?.AssignDoor(LeftRoom?.door);
		cardReaderRight?.AssignDoor(door);

		checkIfRoomHasACamera();
	}

	void checkIfRoomHasACamera()
	{
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Room"))
		{
			if (obj.name.Equals("Security Room"))
			{
				cameraRoom = obj.GetComponent<CameraRoom>();
			}
		}

		foreach (Transform t in gameObject.transform)
		{
			if (t.name.Equals("Camera"))
			{
				roomHasCamera = true;
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

	void OnMouseDown()
	{
		printNumberOfGameObjects();

		door.Close();
	}

	void printGameObjects()
	{
		foreach (GameObject g in charactersInRoom)
		{
			Debug.Log(g.ToString());
		}

		foreach (GameObject g in npcsInRoom)
		{
			Debug.Log(g.ToString());
		}
	}
	void printNumberOfGameObjects()
	{
		Debug.Log(charactersInRoom.Count + npcsInRoom.Count);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.isTrigger)
		{
			if (other.CompareTag("Player"))
			{
				charactersInRoom.Add(other.gameObject);
				if (roomHasCamera)
				{
					cameraRoom.AlertGuard();
				}
			}
			if (other.CompareTag("NPC"))
			{
				npcsInRoom.Add(other.gameObject);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			charactersInRoom.Remove(other.gameObject);
		}
		if (other.CompareTag("NPC"))
		{
			npcsInRoom.Remove(other.gameObject);
		}
	}

	public bool SelectedPlayerInRoom()
	{
		if (Character.selectedCharacter != null)
		{
			if (this.charactersInRoom.Contains(Character.selectedCharacter.gameObject))
			{
				return true;
			}
		}
		return false;
	}

	public bool AnyCharacterInRoom()
	{
		if (this.charactersInRoom.Count > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public HashSet<GameObject> getNPCsInRoom()
	{
		return npcsInRoom;
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

	public CardReader GetCardReaderLeft()
	{
		return this.cardReaderLeft;
	}

	public CardReader GetCardReaderRight()
	{
		return this.cardReaderRight;
	}

	public StairsBehaviour getStairScript()
	{
		return gameObject.GetComponent<StairsBehaviour>();
	}

	public bool isRoom()
	{
		return !gameObject.GetComponent<StairsBehaviour>();
	}
}