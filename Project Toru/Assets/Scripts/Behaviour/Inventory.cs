using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

	private List<Item> inv;
	private static InventoryUI INVUI = new InventoryUI();

	public Inventory()
	{
		inv = new List<Item>();
	}

	public void addItem(Item item)
	{
		inv.Add(item);
		INVUI.showInv();
	}

	public void removeItem(Item item)
	{
		inv.Remove(item);
	}

	public List<Item> getItemsList()
	{
		return inv;
	}
}
