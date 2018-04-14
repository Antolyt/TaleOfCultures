using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

    public bool useSavegameData;

    List<string> plantIds;

    void Start () {


        plantIds = new List<string>();

        foreach (Plant plant in GetComponentsInChildren<Plant>())
        {
            plantIds.Add(plant.data.id);
        }

        // Set plants on field from saveData
        if (Savegame.savegameData != null)
        {
            foreach (PlantData pd in Savegame.savegameData.plants)
            {
                int i = plantIds.FindIndex(a => a == pd.id);
                if (i == -1)
                {
                    GameObject plant = Plant.CreateObject(pd);
                    plant.transform.parent = transform;
                }
                else if (useSavegameData)
                {
                    Plant plant = this.transform.GetChild(i).GetComponent<Plant>();
                    plant.data = pd;
                    plant.SetPlantState();
                }
            }
        }
	}


}