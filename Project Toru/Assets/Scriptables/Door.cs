using Assets.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scriptables { 

	[CreateAssetMenu(fileName = "new Furniture", menuName = "Furniture/Door")]
	public class Door : ScriptableFurniture
	{
		public ScriptableRoom source;
		public ScriptableRoom destination;
		public Locklevel open;
		public Door(Sprite sprite) : base(sprite)
		{
		}

		public void move()
		{

		}

		public List<ScriptableRoom> calculatePath(ScriptableRoom dest, List<ScriptableRoom> route) { return destination.CalculatePath(dest, route); }
	}
	public enum Locklevel
	{
		opened,
		unlocked,
		locked
	}

}
