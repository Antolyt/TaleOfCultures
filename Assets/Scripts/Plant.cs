using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int age;                 // age of the plant
    public SpriteRenderer sR;       // sprite renderer from current gameObject
    public int fruitsCount;         // number of plants, which can be collected
    public int value;               // value of plant, if above threshold higher ranked fruit
    public PlantState[] plantStates;
    private int currentPlantState;

    private void Start()
    {
        if(plantStates.Length > 0)
        {
            sR.sprite = plantStates[currentPlantState].sprite;
        }
    }

    public void IncreaseAge()
    {
        age++;
        if(currentPlantState < plantStates.Length - 1 && age >= plantStates[currentPlantState+1].requiredAge)
        {
            currentPlantState++;
            sR.sprite = plantStates[currentPlantState].sprite;
        }
    }
}

[System.Serializable]
public struct PlantState
{
    public Sprite sprite;
    public int requiredAge;
}
