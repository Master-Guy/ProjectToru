using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Room = Assets.Application.Room;

namespace Assets.Scriptables { 

	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Door")]
	class Door : Furniture
	{
		public Room source;
		public Room Destination;
		public Locklevel open;
		public Door(Sprite sprite) : base(sprite)
		{
		}

		public void move()
		{

		}
	}
	enum Locklevel
	{
		opened,
		unlocked,
		locked
	}

}
