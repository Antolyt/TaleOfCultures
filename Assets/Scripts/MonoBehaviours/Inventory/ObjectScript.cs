using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[Serializable]
public class ObjectData
{
    public string name;
    public string id;
    public int posX;
    public int posY;

    public ObjectData(string name, string id, Vector3 position)
    {
        this.name = name;
        this.id = id;
        this.posX = (int)Math.Round(position.x);
        this.posY = (int)Math.Round(position.y);
    }

    public Vector3 Position()
    {
        return new Vector3(posX, posY);
    }
}