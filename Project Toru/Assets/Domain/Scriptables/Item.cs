using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Option = Assets.Domain.Option;

namespace Assets.Scriptables
{
	[CreateAssetMenu(fileName = "new Item", menuName = "Item")]
	public class Item : ScriptableObject
	{
		public new string name;
		public List<Option> options;
	}
}
