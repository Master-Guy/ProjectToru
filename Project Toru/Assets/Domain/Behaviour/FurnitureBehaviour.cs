using UnityEngine;
using System.Collections;
using ScriptableFurniture = Assets.Scriptables.ScriptableFurniture;

namespace Assets.Domain
{
	public class Furniture : MonoBehaviour
	{
		public ScriptableFurniture info;

		public Furniture(ScriptableFurniture info)
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
