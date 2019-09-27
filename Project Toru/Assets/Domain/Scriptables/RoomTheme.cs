using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new RoomTheme", menuName = "RoomTheme")]
public class RoomTheme : ScriptableObject
{
	public new string name;

	public TileBase topLeft;
	public TileBase left;
	public TileBase bottomLeft;

	public TileBase topRight;
	public TileBase right;
	public TileBase bottomRight;

	public TileBase top;
	public TileBase bottom;
	public TileBase center;
}
