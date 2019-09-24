using Assets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
	class ItemObject : ScriptableObject
	{
		public string sprite;
		public string name;
		public bool uncovered;
		public Dictionary<string, Delegate> options;
	}
}
