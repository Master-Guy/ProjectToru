using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using Assets.Scripts.Behaviour;
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
}