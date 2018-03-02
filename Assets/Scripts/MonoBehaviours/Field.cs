using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

	void Start () {
        // Set plants on field from saveData
        if (Savegame.savegame && Savegame.savegameData != null)
        {
            int i = -1;
            foreach (PlantData pd in Savegame.savegameData.plants)
            {
                GameObject go = Resources.Load<GameObject>("Prefabs/Plants/" + pd.type.ToString());
                GameObject plantToPlace = GameObject.Instantiate(go);
                plantToPlace.name = pd.type.ToString() + i--;
                plantToPlace.transform.parent = transform;

                plantToPlace.transform.position = new Vector3(pd.posX, pd.posY);
                plantToPlace.GetComponent<SpriteRenderer>().sortingOrder = -(int)plantToPlace.transform.position.y;

                plantToPlace.GetComponent<Plant>().SetFromPlantData(pd);
            }
        }
	}


}