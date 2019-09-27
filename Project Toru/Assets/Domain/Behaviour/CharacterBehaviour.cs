using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scriptables;

namespace Assets.Domain
{

	public class Character : MonoBehaviour
	{
		public ScriptableCharacter info;

		public Character(ScriptableCharacter info)
		{
			this.info = info;
		}

		void Start()
		{

		}

		void Update()
		{

		}
	}
}
