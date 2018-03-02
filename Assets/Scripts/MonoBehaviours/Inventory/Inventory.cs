using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public int availableSize;
    public InventoryItem[] inventoryItems;
    public GameObject blendScreen;

    public bool itemSelected;
    public int selectedItem;

    private string[] sorting = { "name", "type" };

    void Start()
    {
        for (int i = availableSize; i < inventoryItems.Length; i++)
        {
            Image ima = inventoryItems[i].itemImage;
            Transform itemSlot = ima.transform.parent;

            Button button = itemSlot.GetComponent<Button>();
            button.interactable = false;
            //inventoryItems[i].itemImage.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }

        // Set inventorySlots from savegame
        if (Savegame.savegame && Savegame.savegameData != null)
        {
            for (int i = 0; i < Savegame.savegameData.inventoryItems.Length; i++)
            {
                ItemData id = Savegame.savegameData.inventoryItems[i];
                if (id != null)
                {
                    Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + id.name);

                    inventoryItems[i].item = item;
                    inventoryItems[i].itemImage.sprite = item.sprite;
                    inventoryItems[i].itemCount.text = id.count.ToString();
                    inventoryItems[i].itemImage.enabled = true;
                }
            }
        }

        Close();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Close();
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
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == item)
            {
                inventoryItems[i].itemCount.text = (Int32.Parse(inventoryItems[i].itemCount.text) + 1).ToString();
                return;
            }
        }

        // add item to first 
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == null)
            {
                inventoryItems[i].item = item;
                inventoryItems[i].itemImage.sprite = item.sprite;
                inventoryItems[i].itemCount.text = count.ToString();
                inventoryItems[i].itemImage.enabled = true;
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
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == item)
            {
                if (inventoryItems[i].itemCount.text == "")
                {
                    inventoryItems[i].item = null;
                    inventoryItems[i].itemImage.sprite = null;
                    inventoryItems[i].itemImage.enabled = false;
                }
                else if (inventoryItems[i].itemCount.text == "2")
                {
                    inventoryItems[i].itemCount.text = "";
                }
                else
                {
                    inventoryItems[i].itemCount.text = (Int32.Parse(inventoryItems[i].itemCount.text) - 1).ToString();
                }

                return;
            }
        }
    }

    /// <summary>
    /// Removes one item at index from inventory
    /// </summary>
    /// <param name="i">index of inventorySlot</param>
    public void RemoveItem(int i)
    {
        if (inventoryItems[i] != null && inventoryItems[i].item != null)
        {
            if (inventoryItems[i].itemCount.text == "")
            {
                inventoryItems[i].item = null;
                inventoryItems[i].itemImage.sprite = null;
                inventoryItems[i].itemImage.enabled = false;
            }
            else if (inventoryItems[i].itemCount.text == "2")
            {
                inventoryItems[i].itemCount.text = "";
            }
            else
            {
                inventoryItems[i].itemCount.text = (Int32.Parse(inventoryItems[i].itemCount.text) - 1).ToString();
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

    /// <summary>
    /// Opens inventory
    /// </summary>
    public void Open()
    {
        blendScreen.SetActive(true);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes Inventory
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
        blendScreen.SetActive(false);
    }
}

/// <summary>
/// Contains information of inventorySlot
/// </summary>
[Serializable]
public class InventoryItem
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
}
