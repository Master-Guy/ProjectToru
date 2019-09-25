using UnityEngine;
using System.Collections;
using ScriptableRoom = Assets.Scriptables.ScriptableRoom;

namespace Assets.Domain
{
	public class Room : Drawable
	{

		public ScriptableRoom info;

		public Room(string sprite, ScriptableRoom info) : base(sprite)
		{
			this.info = info;
		}
		public override void Draw()
		{
			throw new System.NotImplementedException();
		}

		void Start()
		{

		}

		void Update()
		{

		}
	}
}

