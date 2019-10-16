using System;
using Assets.Domain;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Room", menuName = "Room")]
/// <summary>
/// This class provides the layout for a room
/// </summary>
/// <remarks>
/// This class is at the heart of the levels, it has a position, background and furniture
/// a room also has a variable denoting if it has been uncovered,
/// in game it will also store which characters are currently present.
/// </remarks>
public class RoomOld : Drawable
{
	// Constants //
	private const int maxCharacters = 10;

	// Variables //
	public Vector3Int position;
	public Theme theme;
	public int width = 1;
	public int height = 1;

	public new string name;
	public bool lightsOn = true;
	public Dictionary<short, Furniture> furniture;
	private List<DoorOld> doors;


	// public Functions //
	/// <summary>
	/// Constructor with custom size
	/// </summary>
	/// <param name="posX"></param>
	/// <param name="posY"></param>
	/// <param name="xSize"></param>
	/// <param name="ySize"></param>
	RoomOld(Vector3Int pos, int xSize, int ySize, string roomTheme) : base(roomTheme)
	{
		position = pos;
		width = xSize;
		height = ySize;
		furniture = new Dictionary<short, Furniture>();
		doors = new List<DoorOld>();
	}
	/// <summary>
	/// Constructor with standard size
	/// </summary>
	/// <remarks>
	/// This function simply calls the custom constructor with 1 as size
	/// </remarks>
	/// <param name="posX"></param>
	/// <param name="posY"></param>
	/// <param name="background"></param>
	RoomOld(Vector3Int pos, string background) : this(pos, 2, 1, background) { }

	/// <summary>
	/// adds a piece of furniture to the room
	/// </summary>
	/// <param name="furn"></param>
	/// <param name="loc"></param>
	void addFurniture(Furniture furn, short loc)
	{
		furniture.Add(loc, furn);
	}

	public List<RoomOld> CalculatePath(RoomOld dest, List<RoomOld> route)
	{
		if (dest == this)
		{
			route.Add(this);
			return route;
		}
		if (!uncovered || route.Contains(this))
			return null;
		route.Add(this);
		List<RoomOld> res = null, temp = null;
		foreach (DoorOld d in doors)
		{
			temp = d.calculatePath(dest, route);
			if ((temp != null && temp.Count < res.Count) || res == null)
				res = temp;
		}
		res.Add(this);
		return res;
	}

	public override void Draw()
	{
		throw new NotImplementedException();
	}
}

enum RoomErrors
{
	eDrawError,
	eRoomFull,
	eCharacterNotFound
}

