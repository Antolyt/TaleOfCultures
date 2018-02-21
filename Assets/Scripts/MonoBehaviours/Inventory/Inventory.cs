using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public InventoryItem[] inventoryItems;

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == itemToAdd)
            {
                inventoryItems[i].itemCount.text = (Int32.Parse(inventoryItems[i].itemCount.text) + 1).ToString();
                return;
            }
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == null)
            {
                inventoryItems[i].item = itemToAdd;
                inventoryItems[i].itemImage.sprite = itemToAdd.sprite;
                inventoryItems[i].itemCount.text = "1";
                inventoryItems[i].itemImage.enabled = true;
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == itemToRemove)
            {
                if (inventoryItems[i].itemCount.text == "1")
                {
                    inventoryItems[i].item = null;
                    inventoryItems[i].itemImage.sprite = null;
                    inventoryItems[i].itemImage.enabled = false;
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
}

[Serializable]
public class InventoryItem
{
    public Image itemImage;
    public Item item;
    public Text itemCount;
}

[Serializable]
public class InventoryData
{
    public ItemData[,] itemData;
}
