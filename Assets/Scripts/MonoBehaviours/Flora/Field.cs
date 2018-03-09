using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

	void Start () {
        // Set plants on field from saveData
        if (Savegame.savegameData != null)
        {
            foreach (PlantData pd in Savegame.savegameData.plants)
            {
                GameObject plant = Plant.CreateObject(pd);
                plant.transform.parent = transform;
            }
        }
	}


}