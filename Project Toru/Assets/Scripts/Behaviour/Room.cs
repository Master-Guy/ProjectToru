﻿using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Room : MonoBehaviour, IPointerClickHandler
{

	// Note: The 
	[SerializeField]
	public Theme theme = null;

	[SerializeField]
	private Tilemap walls = null;

	[SerializeField]
	private Tilemap background = null;

	[SerializeField]
	private bool lightsOn;

	[SerializeField]
	public Vector2 size = new Vector2(1, 1);

    public HashSet<GameObject> charactersInRoom;
    public HashSet<NPC> npcsInRoom;

    public Room()
    {
        charactersInRoom = new HashSet<GameObject>();
        npcsInRoom = new HashSet<NPC>();
    }

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInRoom.Add(other.gameObject);
        }
        if (other.CompareTag("NPC"))
        {
            npcsInRoom.Add(other.gameObject.GetComponent<NPC>());
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
            npcsInRoom.Remove(other.gameObject.GetComponent<NPC>());
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

        foreach (NPC npc in npcsInRoom)
        {
            Debug.Log(npc.ToString());
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
}