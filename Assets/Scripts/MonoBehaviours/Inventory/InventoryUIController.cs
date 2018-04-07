using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public PlayerController pc;
    public GameObject blendScreen;

    public Inventory inventory;
    public Transform inventoryTransform;
    public Transform inventoryInventoryUIPosition;

    #region QuickSlots
    public QuickSlots quickSlots;
    public Transform quickSlotTransform;
    public Transform quickSlotGameplayUIPosition;
    public Transform quickSlotInventoryUIPosition;
    #endregion

    private void Start()
    {
        inventory.Initilize();
        quickSlots.Initialize();
        Close();
    }

    // Update is called once per frame
    void Update () {
        #region QuickSlot
        // if a item is selected in the inventory put it on the pressed quickSlot
        if (inventory.itemSelected)
        {
            if (Input.GetButtonDown("QuickSlotUp"))
            {
                quickSlots.SetItem(0);
                inventory.itemSelected = false;
            }
            if (Input.GetButtonDown("QuickSlotRight"))
            {
                quickSlots.SetItem(1);
                inventory.itemSelected = false;
            }
            if (Input.GetButtonDown("QuickSlotDown"))
            {
                quickSlots.SetItem(2);
                inventory.itemSelected = false;
            }
            if (Input.GetButtonDown("QuickSlotLeft"))
            {
                quickSlots.SetItem(3);
                inventory.itemSelected = false;
            }
        }
        #endregion

        if (Input.GetButtonDown("Inventory"))
        {
            Close();
            pc.enabled = true;
        }
    }


    /// <summary>
    /// Opens inventory
    /// </summary>
    public void Open()
    {
        pc.enabled = false;

        quickSlotTransform.SetParent(quickSlotInventoryUIPosition);
        quickSlotTransform.localPosition = Vector3.zero;
        inventoryTransform.SetParent(inventoryInventoryUIPosition);
        inventoryTransform.localPosition = Vector3.zero;
        blendScreen.SetActive(true);
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Closes Inventory
    /// </summary>
    public void Close()
    {
        quickSlotTransform.SetParent(quickSlotGameplayUIPosition);
        quickSlotTransform.localPosition = Vector3.zero;
        blendScreen.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
