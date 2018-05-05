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
}

[CreateAssetMenu(fileName = "Tool", menuName = "Item/Tool", order = 1)]
public class Toll : Item
{

}

public class Seed : Item
{

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
