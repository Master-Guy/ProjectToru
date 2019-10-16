﻿using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class RoomBehaviour : MonoBehaviour, IPointerClickHandler
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
	public Vector2Int size = new Vector2Int(0, 0);

	void Start()
	{
		if (size.x == 0 || size.y == 0)
		{
			Debug.LogWarning("A room size must be set manualy");
		}
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
}