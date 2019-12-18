using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Assign a Item List
    private HashSet<Item> inv;

    //Create's a static InventoryUI manager
    private InventoryUI INVUI;

    public float MaxWeight;

    //Constructor - Creates a Item List
    public Inventory()
    {
        inv = new HashSet<Item>();
    }

    public void Start()
    {
        INVUI = gameObject.AddComponent(typeof(InventoryUI)) as InventoryUI;
    }

    public void SetMaxWeight(float MaxWeight)
    {
        this.MaxWeight = MaxWeight;
    }

    //Add an item to the Inventory
    public void addItem(Item item)
    {
        if (inv.Count < INVUI.allSlots && (getWeightOfInventory() + item.Weight) <= MaxWeight)
        {
            Debug.Log("1");
            bool Found = false;
            if (item.isStackable)
            {

                Debug.Log("2");
                foreach (Item i in inv)
                {

                    Debug.Log("3");
                    if (i.GetType().Equals(item.GetType()) && !Found)
                    {
                        i.value += item.value;
                        Found = true;

                        Debug.Log("4");
                    }
                }
            }

            if (!Found)
            {
                inv.Add(item);

                Debug.Log("5");
            }

            UpdateUI();
            Destroy(item.gameObject);
        }

        Debug.Log("6");
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

        foreach (Item i in inv)
        {
            currentWeight += i.Weight;
        }

        return currentWeight;
    }

    public float getMoney()
    {
        foreach (Item i in inv)
        {
            if (i.GetType().Name == "Money")
            {
                return i.value;
            }
        }
        return 0;
    }
}
