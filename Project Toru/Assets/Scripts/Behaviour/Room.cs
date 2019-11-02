using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Room : MonoBehaviour, IPointerClickHandler
{

	[SerializeField]
	private Tilemap walls = null;

	[SerializeField]
	private Tilemap background = null;

	[SerializeField]
	private bool lightsOn;

	[SerializeField]
	public Vector2 size = new Vector2(1, 1);

    private HashSet<GameObject> charactersInRoom;
    private HashSet<GameObject> npcsInRoom;

    public Room()
    {
        charactersInRoom = new HashSet<GameObject>();
        npcsInRoom = new HashSet<GameObject>();
    }

	void Start()
	{

	}

	void Update()
	{

	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log("Right Mouse Button Clicked on: " + name);
		}
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInRoom.Add(other.gameObject);
        }
        if (other.CompareTag("NPC"))
        {
            npcsInRoom.Add(other.gameObject);
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

    void OnMouseDown()
    {
        printNumberOfGameObjects();
    }

    void printGameObjects()
    {
        foreach(GameObject g in charactersInRoom)
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

	public bool SelectedPlayerInRoom()
	{
		if (this.charactersInRoom.Contains(Character.selectedCharacter))
		{
			return true;
		}
		else
		{
			return false;
		}
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
}