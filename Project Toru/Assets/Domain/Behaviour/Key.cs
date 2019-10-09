using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Domain.Behaviour
{
	public class Key : Item
	{
		public int privateKey;

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.CompareTag("Player"))
			{
				collision.GetComponent<Character>().addItem(this);
				this.gameObject.SetActive(false);
				Debug.Log("You picked up a key");
			}
		}
	}
}
