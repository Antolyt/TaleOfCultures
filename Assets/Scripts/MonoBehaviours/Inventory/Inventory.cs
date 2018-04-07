using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public QuickSlots quickSlots;

    public int availableSize;
    public UIItem[] uiItems;

    public bool itemSelected;
    public int selectedItem;

    //private string[] sorting = { "name", "type" };

    public void Initilize()
    {
        for (int i = availableSize; i < uiItems.Length; i++)
        {
            Image ima = uiItems[i].itemImage;
            Transform itemSlot = ima.transform.parent;

            Button button = itemSlot.GetComponent<Button>();
            button.interactable = false;
        }

        // Set inventorySlots from savegame
        if (Savegame.savegameData != null)
        {
            for (int i = 0; i < Savegame.savegameData.inventoryItems.Length; i++)
            {
                ItemData id = Savegame.savegameData.inventoryItems[i];
                if (id != null)
                {
                    Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + id.name);

                    uiItems[i].item = item;
                    uiItems[i].itemImage.sprite = item.sprite;
                    if(id.count > 1) uiItems[i].itemCount.text = id.count.ToString();
                    uiItems[i].itemImage.enabled = true;
                }
            }
        }
    }

    /// <summary>
    /// Adds one item to first empty inventorySlot or increases number if existing
    /// </summary>
    /// <param name="item">item to add</param>
    public void AddItem(Item item)
    {
        AddItem(item, 1);
    }

    /// <summary>
    /// Adds n item to first empty inventorySlot or increases number if existing
    /// </summary>
    /// <param name="item">item to add</param>
    /// <param name="count">number of items</param>
    public void AddItem(Item item, int count)
    {
        // check if item exist and increase number
        for (int i = 0; i < uiItems.Length; i++)
        {
            if (uiItems[i].item == item)
            {
                int newCount = uiItems[i].itemCount.text == "" ? 1 + count : int.Parse(uiItems[i].itemCount.text) + count;
                uiItems[i].itemCount.text = newCount.ToString();

                // Update number of items in quickSlots if referenced
                for(int j = 0; j < QuickSlots.SIZE; j++)
                {
                    if (quickSlots.inventoryReference[j] == i)
                    {
                        quickSlots.UpdateItemCount(j, uiItems[i]);
                    }
                }

                return;
            }
        }

        // add item to first 
        for (int i = 0; i < uiItems.Length; i++)
        {
            if (uiItems[i].item == null)
            {
                uiItems[i].item = item;
                uiItems[i].itemImage.sprite = item.sprite;
                if(count > 1) uiItems[i].itemCount.text = count.ToString();
                uiItems[i].itemImage.enabled = true;
                return;
            }
        }
    }

    /// <summary>
    /// Removes one of item form inventory
    /// </summary>
    /// <param name="item">item to Remove</param>
    public void RemoveItem(Item item)
    {
        for (int i = 0; i < uiItems.Length; i++)
        {
            if (uiItems[i].item == item)
            {
                if (uiItems[i].itemCount.text == "")
                {
                    RemoveItem(i);
                    quickSlots.ClearItem(i);
                }
                else if (uiItems[i].itemCount.text == "2")
                {
                    uiItems[i].itemCount.text = "";
                }
                else
                {
                    uiItems[i].itemCount.text = (Int32.Parse(uiItems[i].itemCount.text) - 1).ToString();
                }

                return;
            }
        }
    }

    /// <summary>
    /// Removes all items at index from inventory
    /// </summary>
    /// <param name="i">index of inventorySlot</param>
    public void RemoveItem(int i)
    {
        uiItems[i].item = null;
        uiItems[i].itemImage.sprite = null;
        uiItems[i].itemImage.enabled = false;
        uiItems[i].itemCount.text = "";
        quickSlots.ClearItem(i);
    }

    /// <summary>
    /// Removes "count" items at index from inventory
    /// </summary>
    /// <param name="i">index of inventorySlot</param>
    /// <param name="count">number of items to remove</param>
    public void RemoveItem(int i, int count)
    {
        if (uiItems[i] != null && uiItems[i].item != null)
        {
            if (int.Parse(uiItems[i].itemCount.text) <= count)
            {
                RemoveItem(i);
            }
            else if (int.Parse(uiItems[i].itemCount.text) == count - 1)
            {
                uiItems[i].itemCount.text = "";
            }
            else
            {
                uiItems[i].itemCount.text = (Int32.Parse(uiItems[i].itemCount.text) - count).ToString();
            }

            return;
        }
    }

    /// <summary>
    /// Select item in inventory at index
    /// </summary>
    /// <param name="i">index of inventorySlot</param>
    public void SetSelectedItem(int i)
    {
        selectedItem = i;
        itemSelected = true;
    }

    /// <summary>
    /// Sorts Inventory
    /// </summary>
    public void Sort()
    {
        //ToDo Implement different sorting alternatives and switch to the next by call of function
    }
}

/// <summary>
/// Contains information of inventorySlot
/// </summary>
[Serializable]
public class UIItem
{
    public Image itemImage;
    public Item item;
    public Text itemCount;

    /// <summary>
    /// Transform inventoryItem to itemData
    /// </summary>
    /// <returns>itemData of inventoryItem</returns>
    public ItemData ToData()
    {
        if (item)
            return new ItemData(item.name, Int32.Parse(itemCount.text));
        else
            return null;
    }

    public bool isNull()
    {
        return itemImage == null || item == null;
    }
}
