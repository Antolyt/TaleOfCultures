using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlots : MonoBehaviour {

    public InventoryItem[] quickSlotItems;
    public int[] inventoryReference;
    public int selectedQuickSlot;

    public Inventory inventory;

    // Use this for initialization
    void Start()
    {
        inventoryReference = new int[4];

        // Set QuickSlots from savegame
        if (Savegame.savegame && Savegame.savegameData != null)
        {
            for (int i = 0; i < Savegame.savegameData.quickSlotItems.Length; i++)
            {
                ItemData id = Savegame.savegameData.quickSlotItems[i];

                if (id != null)
                {
                    Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + id.name);

                    quickSlotItems[i].item = item;
                    quickSlotItems[i].itemImage.sprite = item.sprite;
                    quickSlotItems[i].itemCount.text = id.count.ToString();
                    quickSlotItems[i].itemImage.enabled = true;
                    inventoryReference[i] = Savegame.savegameData.inventoryQuickSlotRef[i];
                }
            }
        }
    }

    public void SetItem(int quickSlot)
    {
        quickSlotItems[quickSlot].item = inventory.inventoryItems[inventory.selectedItem].item;
        quickSlotItems[quickSlot].itemImage.sprite = inventory.inventoryItems[inventory.selectedItem].itemImage.sprite;
        quickSlotItems[quickSlot].itemCount.text = inventory.inventoryItems[inventory.selectedItem].itemCount.text;
        quickSlotItems[quickSlot].itemImage.enabled = true;
        inventoryReference[quickSlot] = inventory.selectedItem;
    }

    // Removes Item in QuickSlots and referenced item in inventory
    public void RemoveItem(int i)
    {
        if (quickSlotItems[i] != null)
        {
            if (quickSlotItems[i].itemCount.text == "")
            {
                quickSlotItems[i].item = null;
                quickSlotItems[i].itemImage.sprite = null;
                quickSlotItems[i].itemImage.enabled = false;
            }
            else if(quickSlotItems[i].itemCount.text == "2")
            {
                quickSlotItems[i].itemCount.text = "";
            }
            else
            {
                quickSlotItems[i].itemCount.text = (Int32.Parse(quickSlotItems[i].itemCount.text) - 1).ToString();
            }
        }

        inventory.RemoveItem(inventoryReference[i]);
    }

    public void RemoveSelectedItem()
    {
        RemoveItem(selectedQuickSlot);
    }

    public void ClearItem(int i)
    {
        if (quickSlotItems[i] != null)
        {
            quickSlotItems[i].item = null;
            quickSlotItems[i].itemImage.sprite = null;
            quickSlotItems[i].itemImage.enabled = false;
            quickSlotItems[i].itemCount.text = "";
        }
    }

    public void SelectQuickSlot(int i)
    {
        selectedQuickSlot = i;
        // ToDo: Feedback
    }

    public InventoryItem GetSelectedItem()
    {
        if (quickSlotItems[selectedQuickSlot] == null || quickSlotItems[selectedQuickSlot].item == null)
            return null;
        return quickSlotItems[selectedQuickSlot];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
