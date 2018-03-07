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