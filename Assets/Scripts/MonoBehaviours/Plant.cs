using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantType
{
    Bush,
    Flower,
    Leave,
    Root,
    Tree
}

public class Plant : MonoBehaviour
{
    public PlantData data;

    public PlantType type;
    private delegate KeyValuePair<int, Item> Harvest();
    private Harvest harvest;

    public SpriteRenderer spriteRenderer;       // sprite renderer from current gameObject
    [SerializeField] private int yieldStartState;
    [SerializeField] private int yieldEndState;
    [SerializeField] private int yieldPerDay;
    [SerializeField] private int maxYield;
    public int value;               // value of plant, if above threshold higher ranked fruit
    [Header("Plant States")]
    public PlantState[] plantStates;
    //public PlantState seed;
    //public PlantState germ;
    //public PlantState small;
    //public PlantState big;
    //public PlantState flower;
    //public PlantState smallFruit;
    //public PlantState fruit;
    //public PlantState dead;
    //public PlantState removed;
    //public PlantState winter;
    private int currentPlantState;

    public Fruit droppedFruit;
    public Seed droppedSeed;

    private void Start()
    {
        data.posX = (int)Math.Round(transform.position.x);
        data.posY = (int)Math.Round(transform.position.y);

        spriteRenderer.sortingOrder = -(int)data.posY;
        SetPlantState();

        switch (type)
        {
            case PlantType.Bush:
                harvest = HarvestBush;
                break;
            case PlantType.Leave:
                harvest = HarvestLeave;
                break;
            case PlantType.Root:
                harvest = HarvestRoot;
                break;
            case PlantType.Tree:
                harvest = HarvestTree;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates age, sprite of plant, and yield
    /// </summary>
    public void IncreaseAge()
    {
        data.age++;
        if(currentPlantState < plantStates.Length - 1 && data.age >= plantStates[currentPlantState+1].requiredAge)
        {
            currentPlantState++;
            spriteRenderer.sprite = plantStates[currentPlantState].sprite;
        }
        if(currentPlantState >= yieldStartState && currentPlantState <= yieldEndState)
        {
            data.yield = data.yield + yieldPerDay > maxYield ? maxYield : data.yield + yieldPerDay;
        }
    }

    public static GameObject CreateObject(PlantData data)
    {
        GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Plants/" + data.name));
        Plant plant = go.GetComponent<Plant>();
        plant.data = data;
        go.transform.position = data.Position();
        plant.spriteRenderer.sortingOrder = -(int)data.posY;
        plant.SetPlantState();

        return go;
    }

    public void SetPlantState()
    {
        for (int i = 0; i < plantStates.Length; i++)
        {
            if (data.age >= plantStates[i].requiredAge)
            {
                continue;
            }
            else
            {
                currentPlantState = Mathf.Max(0, i - 1);
                spriteRenderer.sprite = plantStates[currentPlantState].sprite;
                break;
            }
        }
    }

    public KeyValuePair<int, Item> HarvestBush()
    {
        KeyValuePair<int, Item> res = new KeyValuePair<int, Item>(data.yield, droppedFruit);
        if (data.yield >= 1)
        {
            SwitchState(5);
            data.yield = 0;
        }

        return res;
    }

    public KeyValuePair<int, Item> HarvestLeave()
    {
        KeyValuePair<int, Item> res = new KeyValuePair<int, Item>(data.yield, droppedFruit);

        // Return seeds if in fruit state
        if (currentPlantState == 5 || currentPlantState == 6) // small fruit/fruit state
        {
            SwitchState(8);
            res = new KeyValuePair<int, Item>(5, droppedSeed);
        }
        // Return fruit if in big/flower state
        else if (data.yield >= 1)
        {
            SwitchState(8);
        }
        return res;
    }

    public KeyValuePair<int, Item> HarvestRoot()
    {
        KeyValuePair<int, Item> res = new KeyValuePair<int, Item>(data.yield, droppedFruit);

        // Return seeds if in fruit state
        if (currentPlantState == 5 || currentPlantState == 6) // small fruit/fruit state
        {
            SwitchState(8);
            res = new KeyValuePair<int, Item>(5, droppedSeed);
        }
        // Return fruit if in big/flower state
        else if (data.yield >= 1)
        {
            SwitchState(8);
        }
        return res;
    }

    public KeyValuePair<int, Item> HarvestTree()
    {
        KeyValuePair<int, Item> res = new KeyValuePair<int, Item>(data.yield, droppedFruit);

        // Return seeds if in fruit state
        if (currentPlantState == 5 || currentPlantState == 6) // small fruit/fruit state
        {
            SwitchState(8);
            res = new KeyValuePair<int, Item>(5, droppedSeed);
        }
        // Return fruit if in big/flower state
        else if (data.yield >= 1)
        {
            SwitchState(8);
        }
        return res;
    }

    public void SwitchState(int state)
    {
        currentPlantState = state;
        spriteRenderer.sprite = plantStates[state].sprite;
    }
}

[Serializable]
public class PlantData : ObjectData
{
    public int age;                 // age of the plant
    public int yield;               // number of plants, which can be collected
    public int value;               // value of plant, if above threshold higher ranked fruit

    public PlantData(string name, Vector3 position) : base(name, position)
    {

    }

    public PlantData(string name, Vector3 position, int age, int yield, int value) : base(name, position)
    {
        this.age = age;
        this.yield = yield;
        this.value = value;
    }
}

[System.Serializable]
public struct PlantState
{
    public Sprite sprite;
    public int requiredAge;
}