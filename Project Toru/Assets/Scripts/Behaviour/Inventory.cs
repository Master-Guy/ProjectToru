using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	//Assign a Item List
	private HashSet<Item> inv;

	//Create's a static InventoryUI manager
	private static InventoryUI INVUI = new InventoryUI();

	//Constructor - Creates a Item List
	public Inventory()
	{
		inv = new HashSet<Item>();
	}

	//Add an item to the Inventory
	public void addItem(Item item)
	{
		inv.Add(item);
		UpdateUI();
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
}
