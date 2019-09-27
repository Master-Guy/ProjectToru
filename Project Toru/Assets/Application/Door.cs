using Assets.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scriptables { 

	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Door")]
	public class Door : Furniture
	{
		public Room source;
		public Room destination;
		public Locklevel open;
		public Door(string sprite) : base(sprite)
		{
		}

		public void move()
		{

		}

		public List<Room> calculatePath(Room dest, List<Room> route) { return destination.CalculatePath(dest, route); }
	}
	public enum Locklevel
	{
		opened,
		unlocked,
		locked
	}

}
