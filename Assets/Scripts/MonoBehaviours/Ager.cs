using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ager : MonoBehaviour {
    public int dayLength;   // day length in seconds

    float timeStemp;
	
	// Update is called once per frame
	void Update () {
		if(Time.time - dayLength > timeStemp)
        {
            foreach(Plant plant in transform.GetComponentsInChildren<Plant>())
            {
                //plant.IncreaseAge();
            }
            timeStemp = Time.time;
        }
	}
}
