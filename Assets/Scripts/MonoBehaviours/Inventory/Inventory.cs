using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public int availableSize;
    public InventoryItem[] inventoryItems;

    public bool itemSelected;
    public int selectedItem;
    //public InventoryItem selectedItem;

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

            //int i = 0;
            //foreach (ItemData id in Savegame.savegameData.inventoryItems)
            //{
            //    if (id != null)
            //    {
            //        Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + id.name);

            //        inventoryItems[i].item = item;
            //        inventoryItems[i].itemImage.sprite = item.sprite;
            //        inventoryItems[i].itemCount.text = id.count.ToString();
            //        inventoryItems[i].itemImage.enabled = true;
            //    }
            //    i++;
            //}
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == item)
            {
                inventoryItems[i].itemCount.text = (Int32.Parse(inventoryItems[i].itemCount.text) + 1).ToString();
                return;
            }
        }

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i].item == null)
            {
                inventoryItems[i].item = item;
                inventoryItems[i].itemImage.sprite = item.sprite;
                inventoryItems[i].itemCount.text = "2";
                inventoryItems[i].itemImage.enabled = true;
                return;
            }
        }
    }

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

    public void SetSelectedItem(int i)
    {
        selectedItem = i;
        itemSelected = true;
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
