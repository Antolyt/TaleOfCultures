using System;
using UnityEngine;

[Serializable]
public class PlantData
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
