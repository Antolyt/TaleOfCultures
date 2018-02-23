using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public int size;
    public InventoryItem[] inventoryItems;

    void Start()
    {
        size = inventoryItems.Length;

        if (Savegame.savegame && Savegame.savegameData != null)
        {
            //for (int i = 0; i < Savegame.savegameData.inventoryItems.Length; i++)
            //{
            //    ItemData id = Savegame.savegameData.inventoryItems[i];
            //    if (id != null)
            //    {
            //        Item item = Resources.t<Fruit>("Prefabs/Fruits/" + id.name);

            //        inventoryItems[i].item = item;
            //        inventoryItems[i].itemImage.sprite = item.sprite;
            //        inventoryItems[i].itemCount.text = id.count.ToString();
            //        inventoryItems[i].itemImage.enabled = true;
            //    }

            //}

            int i = 0;
            foreach (ItemData id in Savegame.savegameData.inventoryItems)
            {
                if (id != null)
                {
                    Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + id.name);

                    inventoryItems[i].item = item;
                    inventoryItems[i].itemImage.sprite = item.sprite;
                    inventoryItems[i].itemCount.text = id.count.ToString();
                    inventoryItems[i].itemImage.enabled = true;
                }
                i++;
            }
        }
    }

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

    public ItemData ToData()
    {
        if (item)
            return new ItemData(item.name, Int32.Parse(itemCount.text));
        else
            return null;
    }
}
