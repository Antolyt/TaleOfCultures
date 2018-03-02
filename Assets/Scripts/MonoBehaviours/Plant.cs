using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlantType
{
    Potato,
    Tomato
}

public class Plant : MonoBehaviour
{
    public PlantType type;
    public int age;                 // age of the plant
    public SpriteRenderer sR;       // sprite renderer from current gameObject
    [Header("Yield & Value")]
    public int yield;               // number of plants, which can be collected
    [SerializeField] private int yieldState;
    [SerializeField] private int yieldPerDay;
    [SerializeField] private int maxYield;
    public int value;               // value of plant, if above threshold higher ranked fruit
    [Header("Plant States")]
    public PlantState[] plantStates;
    private int currentPlantState;

    public Fruit droppedFruit;

    private void Start()
    {
        if(plantStates.Length > 0)
        {
            sR.sprite = plantStates[currentPlantState].sprite;
        }
    }

    /// <summary>
    /// Updates age, sprite of plant, and yield
    /// </summary>
    public void IncreaseAge()
    {
        age++;
        if(currentPlantState < plantStates.Length - 1 && age >= plantStates[currentPlantState+1].requiredAge)
        {
            currentPlantState++;
            sR.sprite = plantStates[currentPlantState].sprite;
        }
        if(currentPlantState == yieldState)
        {
            yield = yield + yieldPerDay > maxYield ? maxYield : yield + yieldPerDay;
        }
    }

    /// <summary>
    /// Transform plant to plantData
    /// </summary>
    /// <returns>plantData of plant</returns>
    public PlantData PlantToData()
    {
        return new PlantData(type, age, yield, value, gameObject.transform.position);
    }

    /// <summary>
    /// Creates plants on basis of plantData
    /// </summary>
    /// <param name="plantData">data to use</param>
    public void SetFromPlantData(PlantData plantData)
    {
        this.age = plantData.age;
        this.yield = plantData.yield;
        this.value = plantData.value;
        for(int i = 0; i < plantStates.Length; i++)
        {
            if(age >= plantStates[i].requiredAge)
            {
                continue;
            }
            else
            {
                currentPlantState = Mathf.Max(0, i - 1);
                sR.sprite = plantStates[currentPlantState].sprite;
                break;
            }
        }
    }
}

[System.Serializable]
public struct PlantState
{
    public Sprite sprite;
    public int requiredAge;
}