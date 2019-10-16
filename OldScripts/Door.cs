using Assets.Domain;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Door")]
public class DoorOld : Furniture
{
	public RoomOld source;
	public RoomOld destination;
	public Locklevel open;
	public DoorOld(string sprite) : base(sprite)
	{
	}

	public void move()
	{

	}

	public List<RoomOld> calculatePath(RoomOld dest, List<RoomOld> route) { return destination.CalculatePath(dest, route); }
}

public enum Locklevel
{
	opened,
	unlocked,
	locked
}
