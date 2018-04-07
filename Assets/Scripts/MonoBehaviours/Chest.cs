using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ObjectScript
{
    public SpriteRenderer spriteRenderer;
    public Sprite closed;
    public Sprite open;
    public ChestData data;

    public void Open()
    {
        spriteRenderer.sprite = open;
    }

    public void Close()
    {
        spriteRenderer.sprite = closed;
    }

    public static void CreateObject(ChestData data)
    {
        GameObject go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(data.name));
        go.transform.position = data.Position();
        Chest chest = go.GetComponent<Chest>();
        chest.data = data;
    }

    public bool AddItem(ItemData item)
    {
        return AddItem(item, item.count);
    }

    public bool AddItem(ItemData item, int count)
    {
        // check if item exist and increase number
        for (int i = 0; i < data.items.Length; i++)
        {
            if (data.items[i] != null && data.items[i].Equals(item))
            {
                data.items[i].count += count;
                return true;
            }
        }

        // add item to first 
        for (int i = 0; i < data.items.Length; i++)
        {
            if (data.items[i] == null || data.items[i].IsNull())
            {
                data.items[i] = new ItemData(item.name, count);
                return true;
            }
        }


        return false;
    }

    public bool RemoveItem(int index)
    {
        return RemoveItem(data.items[index]);
    }

    public bool RemoveItem(ItemData item)
    {
        return RemoveItem(item, item.count);
    }

    private bool RemoveItem(ItemData item, int count)
    {
        // check if item exist and increase number
        for (int i = 0; i < data.items.Length; i++)
        {
            if (data.items[i] != null && data.items[i].Equals(item))
            {
                if (count < data.items[i].count)
                    data.items[i].count -= count;
                else
                    data.items[i] = null;
                return true;
            }
        }

        return false;
    }
}

[Serializable]
public class ChestData : ObjectData
{
    public ItemData[] items;

    public ChestData(string name, Vector3 position, int size) : base(name, position)
    {       
        items = new ItemData[size];
    }
}