using UnityEngine;
using System.Collections;
using ScriptableRoom = Assets.Scriptables.ScriptableRoom;

namespace Assets.Domain
{

	public class Room : MonoBehaviour
	{

		public ScriptableRoom info;

		public Room(string sprite, ScriptableRoom info)
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

