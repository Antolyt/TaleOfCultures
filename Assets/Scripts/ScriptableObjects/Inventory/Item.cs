using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Fruit", menuName = "Item/Fruit", order = 1)]
public class Fruit : Item
{
    ItemRanking fruitRanking;

    public int value;
    public string rank;
    public int count;
}

[CreateAssetMenu(fileName = "ItemRanking", menuName = "Item/Ranking", order = 1)]
public class ItemRanking : ScriptableObject
{
    public ShelfLife[] shelfLife;
}

[Serializable]
public class ShelfLife
{
    public string rank;
    public int requiredValue;
}

[Serializable]
public class ItemData
{
    public string name;
    public int value;
    public int count;

    public ItemData(string name, int count, int value)
    {

    }
}
