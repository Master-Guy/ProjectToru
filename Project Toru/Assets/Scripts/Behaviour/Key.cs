using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Key : Item
{

	// Key access is defined by color
	public CardReader.CardreaderColor color = CardReader.CardreaderColor.Yellow;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && collision.isTrigger)
		{
			collision.GetComponent<Character>().inventory.addItem(this);
		}
	}
}
