﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public abstract class AbRoom : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField]
    //Tilemap walls = null;

    //[SerializeField]
    //Tilemap background = null;

    //[SerializeField]
    //bool lightsOn = true;

    [SerializeField]
    WallController wallController = null;

    public Room LeftRoom = null;
    public Room RightRoom = null;

    public Door door = null;

    public HashSet<GameObject> charactersInRoom;
    public HashSet<GameObject> npcsInRoom;

    [SerializeField]
    Vector2Int size = new Vector2Int(0, 0);

    bool roomHasCamera = false;
    CameraRoom cameraRoom;

    public AbRoom()
    {
        charactersInRoom = new HashSet<GameObject>();
        npcsInRoom = new HashSet<GameObject>();
    }

    void Start()
    {
        if (size.x == 0 || size.y == 0)
        {
            Debug.LogError("A room size must be set manualy");
        }

        if (LeftRoom != null && wallController != null)
        {
            wallController.EnableLeftWall(false);
        }

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            charactersInRoom.Add(other.gameObject);

            if (roomHasCamera)
            {
                Debug.Log("Enterd room with Camera");
                cameraRoom.AlertGuard();
            }
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

    public bool SelectedPlayerInRoom()
    {
        if (this.charactersInRoom.Contains(Character.selectedCharacter.gameObject))
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