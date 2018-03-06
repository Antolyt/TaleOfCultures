using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public SpriteRenderer sRenderer;
    public Sprite closed;
    public Sprite open;
    public ChestData data;

    public void Initialize()
    {
        data.id = ++Savegame.savegameData.chestIdCounter;
        data.x = (int)Math.Round((double)this.transform.position.x);
        data.y = (int)Math.Round((double)this.transform.position.y);
        sRenderer.sortingOrder = -data.y;
    }

    public void Open()
    {
        sRenderer.sprite = open;
    }

    public void Close()
    {
        sRenderer.sprite = closed;
    }
}

[Serializable]
public class ChestData
{
    public int id;

    // position
    public int x;
    public int y;

    public ItemData[] items;

    public ChestData(int id, int x, int y, int size)
    {
        this.id = id;
        this.x = x;
        this.y = y;

        items = new ItemData[size];
    }
}
