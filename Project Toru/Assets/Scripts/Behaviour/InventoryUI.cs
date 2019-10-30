using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
	private GameObject inventory;

	private int allSlots;
	private int enabledSlots;
	private GameObject[] slots;

	public GameObject slotHolder;

    public InventoryUI()
	{
		//Get the objects needed to create the UI
		inventory = GameObject.FindGameObjectWithTag("Inventory");
		slotHolder = GameObject.FindGameObjectWithTag("InventorySlotHolder");

		//Collects and create the slots needed
		allSlots = slotHolder.transform.childCount;
		slots = new GameObject[allSlots];

		//For loop to set the GameObjects
		for (int i = 0; i < allSlots; i++)
		{
			slots[i] = slotHolder.transform.GetChild(i).gameObject;
		}

		//Hides the inventory on Scene load
		inventory.SetActive(false);
	}

	public void showInv(List<Item> inv)
	{
		//Character has his own inventory - Call function to set the inventory to the Inventory
		addInventoryToUI(inv);

		//Show inventory
		inventory.SetActive(true);
	}

	public void hideInv(List<Item> inv)
	{
		//Character has his own inventory - Call function to set the inventory to the Inventory
		addInventoryToUI(inv);

		//Hide inventory
		inventory.SetActive(false);
	}

	public void addInventoryToUI(List<Item> inv)
	{
		for (int i = 0; i < inv.Count; i++)
		{
			//NEEDS AN IF - If there are more items it will not show (SCROLL OBJECT) 
			slots[i].GetComponentInChildren<Image>().sprite = inv[i].UIIcon;
			slots[i].GetComponent<Mask>().showMaskGraphic = true;
		}
	}
}
