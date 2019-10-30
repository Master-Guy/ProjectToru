using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	private GameObject inventory;

	private int allSlots;
	private int enabledSlots;
	private GameObject[] slots;

	public GameObject slotHolder;

    public InventoryUI()
	{
		Debug.Log("Ja");

		allSlots = 5;
		inventory = GameObject.FindGameObjectWithTag("Inventory");
		slotHolder = GameObject.FindGameObjectWithTag("InventorySlotHolder");
		slots = new GameObject[allSlots];

		for (int i = 0; i < allSlots; i++)
		{
			slots[i] = slotHolder.transform.GetChild(i).gameObject;
		}
	}

	public void showInv()
	{
		inventory.SetActive(true);
		//Invoke("hideInv", 0.5f);
	}

	private void hideInv()
	{
		inventory.SetActive(false);
	}
}
