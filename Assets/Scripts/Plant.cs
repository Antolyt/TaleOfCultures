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

    public PlantData PlantToData()
    {
        return new PlantData(type, age, fruitsCount, value, gameObject.transform.position);
    }

    public void SetFromPlantData(PlantData plantData)
    {
        this.age = plantData.age;
        this.fruitsCount = plantData.fruitsCount;
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

[System.Serializable]
public struct PlantData
{
    public PlantType type;
    public int age;                 // age of the plant
    public int fruitsCount;         // number of plants, which can be collected
    public int value;               // value of plant, if above threshold higher ranked fruit
    public float posX;
    public float posY;

    public PlantData(PlantType type, int age, int fruitsCount, int value, Vector3 position)
    {
        this.type = type;
        this.age = age;
        this.fruitsCount = fruitsCount;
        this.value = value;
        this.posX = position.x;
        this.posY = position.y;
    }
}