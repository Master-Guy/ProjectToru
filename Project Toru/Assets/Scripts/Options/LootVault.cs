using Assets.Scripts.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Options
{
	public class LootVault : Option
	{
		private Furniture container;

		public void Start()
		{
			container = GetComponentInParent<Furniture>();
		}

		public override void Activate(Character C)
		{
			foreach(Item i in container.items)
				C.inventory.addItem(i);
		}
	}
}
