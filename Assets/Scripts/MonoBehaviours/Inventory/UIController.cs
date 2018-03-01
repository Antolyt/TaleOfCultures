using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public QuickSlots quickSlots;
    public Inventory inventory;
	
	// Update is called once per frame
	void Update () {
        #region QuickSlot
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
    }
}
