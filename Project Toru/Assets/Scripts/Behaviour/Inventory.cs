﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	//Assign a Item List
	private HashSet<Item> inv;

	//Create's a static InventoryUI manager
	private static InventoryUI INVUI = new InventoryUI();

	public float MaxWeight;

	//Constructor - Creates a Item List
	public Inventory(float MaxWeight)
	{
		this.MaxWeight = MaxWeight;

		inv = new HashSet<Item>();
	}

	//Add an item to the Inventory
	public void addItem(Item item)
	{
		if (inv.Count < INVUI.allSlots && (getWeightOfInventory() + item.Weight) <= MaxWeight)
		{
			bool Found = false;
			foreach (Item i in inv)
			{
				if (i is Money && !Found)
				{
					i.value += item.value;
					Found = true;
				}
			}

			if (!Found)
			{
				inv.Add(item);
			}

			UpdateUI();
			Destroy(item.gameObject);

			foreach(Item i in inv)
			{
				Debug.Log("Name: " + i.name + " - Value: " + i.value);
			}
		}
	}

	//Removes an item from the inventory
	public void removeItem(Item item)
	{
		inv.Remove(item);
		UpdateUI();
	}

	//Get the list of Items
	public HashSet<Item> getItemsList()
	{
		return inv;
	}

	//If needed show the UI
	public void UpdateUI()
	{
		if (inv.Count == 0)
		{
			INVUI.hideInv(inv);
		} 
		else
		{
			INVUI.showInv(inv);
		}
	}

	private float getWeightOfInventory()
	{
		float currentWeight = 0;

		foreach(Item i in inv)
		{
			currentWeight += i.Weight;
		}

		return currentWeight;
	}
}
