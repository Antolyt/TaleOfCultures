using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChestUIController : MonoBehaviour
{
    public PlayerController pc;
    public EventSystem eventSystem;

    public GameObject blendScreen;
    public Inventory inventory;
    public Transform inventoryTransform;
    public Transform inventoryChestUIPosition;
    public UIItem[] uiItems;

    public bool itemSelected;
    public int selectedItemIndex;

    private Chest activeChest;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
            Close();
            pc.enabled = true;
        }

        if (Input.GetButtonDown("Submit"))
        {
            // get selected Item index and parent
            string parentName = eventSystem.currentSelectedGameObject.transform.parent.name;
            selectedItemIndex = int.Parse(eventSystem.currentSelectedGameObject.name.Split('t')[2]);
            if(parentName == "Inventory")
            {
                // Transfer whole itenStack to chest
                UIItem selectedUIItem = inventory.uiItems[selectedItemIndex];
                activeChest.AddItem(new ItemData(selectedUIItem.item.name, int.Parse(selectedUIItem.itemCount.text)));
                inventory.RemoveItem(selectedItemIndex);

                UpdateUI();
            }
            // case Chest
            else
            {
                // Transfer whole itenStack to inventory
                UIItem selectedUIItem = uiItems[selectedItemIndex];
                inventory.AddItem(selectedUIItem.item, int.Parse(selectedUIItem.itemCount.text));
                activeChest.RemoveItem(selectedItemIndex);

                UpdateUI();
            }
        }
    }

    public void Open(Chest chest)
    {
        pc.enabled = false;

        inventoryTransform.SetParent(inventoryChestUIPosition);
        inventoryTransform.localPosition = Vector3.zero;
        this.gameObject.SetActive(true);
        blendScreen.SetActive(true);

        activeChest = chest;
        activeChest.Open();

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < activeChest.data.items.Length && i < uiItems.Length; i++)
        {
            ItemData itemData = activeChest.data.items[i];
            if (itemData != null && !itemData.IsNull())
            {
                Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + itemData.name);

                uiItems[i].item = item;
                uiItems[i].itemImage.sprite = item.sprite;
                if (itemData.count > 1) uiItems[i].itemCount.text = itemData.count.ToString();
                uiItems[i].itemImage.enabled = true;
            } else
            {
                uiItems[i].item = null;
                uiItems[i].itemImage.sprite = null;
                uiItems[i].itemImage.enabled = false;
                uiItems[i].itemCount.text = "";
            }
        }
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
        blendScreen.SetActive(false);

        activeChest.Close();
        activeChest = null;
    }

    public void SlotClick(GameObject go)
    {

    }
}
