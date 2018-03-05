using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlots : MonoBehaviour {

    public const int SIZE = 4;

    public InventoryItem[] quickSlotItems;
    public int[] inventoryReference;
    public int selectedQuickSlot;

    public Inventory inventory;

    // Use this for initialization
    public void Initialize()
    {
        inventoryReference = new int[SIZE];
        for(int i = 0; i < SIZE; i++)
        {
            inventoryReference[i] = -1;
        }

        // Set QuickSlots from savegame
        if (Savegame.savegame && Savegame.savegameData != null)
        {
            for (int i = 0; i < SIZE; i++)
            {
                inventoryReference[i] = Savegame.savegameData.inventoryQuickSlotRef[i];
                if (inventoryReference[i] >= 0)
                {
                    quickSlotItems[i].item = inventory.inventoryItems[inventoryReference[i]].item;
                    quickSlotItems[i].itemImage.sprite = inventory.inventoryItems[inventoryReference[i]].itemImage.sprite;
                    quickSlotItems[i].itemCount.text = inventory.inventoryItems[inventoryReference[i]].itemCount.text;
                    quickSlotItems[i].itemImage.enabled = true;

                    //ItemData id = Savegame.savegameData.inventoryItems[inventoryReference[i]];
                    //Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + id.name);

                    //quickSlotItems[i].item = item;
                    //quickSlotItems[i].itemImage.sprite = item.sprite;
                    //if(id.count > 1) quickSlotItems[i].itemCount.text = id.count.ToString();
                    //quickSlotItems[i].itemImage.enabled = true;
                    
                }
            }
        }
    }

    /// <summary>
    /// Sets item in quickSlot and reference to inventory
    /// </summary>
    /// <param name="i">index of quickSlot</param>
    public void SetItem(int i)
    {
        quickSlotItems[i].item = inventory.inventoryItems[inventory.selectedItem].item;
        quickSlotItems[i].itemImage.sprite = inventory.inventoryItems[inventory.selectedItem].itemImage.sprite;
        quickSlotItems[i].itemCount.text = inventory.inventoryItems[inventory.selectedItem].itemCount.text;
        quickSlotItems[i].itemImage.enabled = true;
        inventoryReference[i] = inventory.selectedItem;
    }

    public void UpdateItemCount(int i, InventoryItem ii)
    {
        quickSlotItems[i].itemCount.text = ii.itemCount.text;
    }

    /// <summary>
    /// Removes one Item in QuickSlots and referenced item in inventory
    /// </summary>
    /// <param name="i">index of quickSlot</param>
    public void RemoveItem(int i)
    {
        // text of number of items either empty or > 2
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

    /// <summary>
    /// Removes item from quickSlots
    /// </summary>
    /// <param name="i">index of quickSlot</param>
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

    /// <summary>
    /// Selects QuickSlot
    /// </summary>
    /// <param name="i">index of quickSlot</param>
    public void SelectQuickSlot(int i)
    {
        selectedQuickSlot = i;
        // ToDo: Feedback
    }

    /// <summary>
    /// Gets the item of selected quickSlot
    /// </summary>
    /// <returns>selected InventoryItem</returns>
    public InventoryItem GetSelectedItem()
    {
        if (quickSlotItems[selectedQuickSlot] == null || quickSlotItems[selectedQuickSlot].item == null)
            return null;
        return quickSlotItems[selectedQuickSlot];
    }
}
