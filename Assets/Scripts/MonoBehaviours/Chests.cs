using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    List<string> chestIds;

    void Start()
    {
        chestIds = new List<string>();

        // Set plants on field from saveData
        if (Savegame.savegameData != null)
        {
            foreach(Chest cd in GetComponentsInChildren<Chest>())
            {
                chestIds.Add(cd.data.id);
            }

            foreach (ChestData cd in Savegame.savegameData.chests)
            {
                int? i = chestIds.FindIndex(a => a == cd.id);
                if (i == null)
                {
                    GameObject plant = Chest.CreateObject(cd);
                    plant.transform.parent = transform;
                }
                else
                {
                    this.transform.GetChild(i.Value).GetComponent<Chest>().data = cd;
                }
            }
        }
    }
}
