using UnityEngine;
using System.Collections;
using ScriptableFurniture = Assets.Scriptables.ScriptableFurniture;

namespace Assets.Domain
{
	public class Furniture : Drawable
	{
		public ScriptableFurniture info;

		public Furniture(string sprite, ScriptableFurniture info) : base(sprite)
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
