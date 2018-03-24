using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {

    public GameObject blendScreen;

    public Inventory inventory;

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
        }
    }


    /// <summary>
    /// Opens inventory
    /// </summary>
    public void Open()
    {
        quickSlotTransform.SetParent(quickSlotInventoryUIPosition);
        quickSlotTransform.localPosition = Vector3.zero;
        blendScreen.SetActive(true);
    }

    /// <summary>
    /// Closes Inventory
    /// </summary>
    public void Close()
    {
        quickSlotTransform.SetParent(quickSlotGameplayUIPosition);
        quickSlotTransform.localPosition = Vector3.zero;
        blendScreen.SetActive(false);
    }
}
