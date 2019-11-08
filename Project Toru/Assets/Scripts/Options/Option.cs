using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Options
{
	public abstract class Option : MonoBehaviour
	{
		[SerializeField]
		private readonly string Description;
		public Skills? Prerequisite = null;


		public string getInfo()
		{
			return Description;
		}

		public abstract void Activate(Character c);
	}
}
