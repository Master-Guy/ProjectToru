using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables;

namespace Assets.Domain
{

	public class Character : Drawable
	{
		public ScriptableCharacter info;

		public Character(string sprite, ScriptableCharacter info) : base(sprite)
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
