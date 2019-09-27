using UnityEngine;
using System.Collections;
using Room = Assets.Scriptables.Room;

namespace Assets.Domain
{

	public class RoomBehaviour : MonoBehaviour
	{

		public RoomBehaviour info;

		public RoomBehaviour(string sprite, RoomBehaviour info)
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

