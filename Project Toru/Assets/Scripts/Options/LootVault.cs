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
		public GameObject money = null;

		private Vault Vault;

		public void Start()
		{
			Vault = GetComponentInParent<Vault>();
		}

		public override string Activate(Character C)
		{
			Vault.Open();
			C.inventory.addItem(money.GetComponent<Money>());
			return null;
		}
	}
}
