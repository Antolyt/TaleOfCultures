using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteCollection", menuName = "Item/Collections/SpriteCollection", order = 1)]
public class SpriteCollection : ScriptableObject
{
    public SpriteColor[] sprites;
}

[Serializable]
public class SpriteColor
{
    public Color color;
    public Sprite sprite;
}