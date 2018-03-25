using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUIController : MonoBehaviour {

    public GameObject blendScreen;
    public GameObject Inventory;
    public UIItem[] uiItems;

    public bool itemSelected;
    public int selectedItem;

    private Chest activeChest;
	
	// Update is called once per frame
	void Update () {

		
	}

    public void Open(Chest chest)
    {
        this.gameObject.SetActive(true);
        blendScreen.SetActive(true);

        activeChest = chest;
        activeChest.Open();

        chest.AddItem(new ItemData("Potato", 10));

        for (int i = 0; i < activeChest.data.items.Length; i++)
        {
            ItemData itemData = activeChest.data.items[i];
            if (itemData != null && !itemData.IsNull())
            {
                Item item = Resources.Load<Fruit>("Prefabs/Fruits/" + itemData.name);

                uiItems[i].item = item;
                uiItems[i].itemImage.sprite = item.sprite;
                if (itemData.count > 1) uiItems[i].itemCount.text = itemData.count.ToString();
                uiItems[i].itemImage.enabled = true;
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
}
