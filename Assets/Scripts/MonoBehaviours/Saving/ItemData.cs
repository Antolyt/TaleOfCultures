using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string name;
    public int count;
    public int value = -1;

    public ItemData(string name, int count, int value)
    {
        this.name = name;
        this.count = count;
        this.value = value;
    }

    public ItemData(string name, int count)
    {
        this.name = name;
        this.count = count;
    }

    public bool Equals(ItemData itemData)
    {
        return this.name == itemData.name && this.value == itemData.value;
    }

    public bool IsNull()
    {
        return name == "";
    }
}