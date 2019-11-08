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

	void Update()
	{

		SpriteRenderer renderer = GetComponentInParent<SpriteRenderer>();

		switch (color)
		{
			case CardReader.CardreaderColor.Blue:
				renderer.color = ColorZughy.cyan;
				break;
			case CardReader.CardreaderColor.Yellow:
				// The color is already yellow
				break;
			case CardReader.CardreaderColor.Purple:
				renderer.color = ColorZughy.purple;
				break;
			case CardReader.CardreaderColor.Disabled:
				Debug.LogError("KEy can't be 'Disabled'");
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && collision.isTrigger)
		{
			collision.GetComponent<Character>().inventory.addItem(this);
		}
	}
}
