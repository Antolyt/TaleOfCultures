using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour {

    public Dictionary<string, GameObject> targets;
    public GameObject target;

    private void Start()
    {
        targets = new Dictionary<string, GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!targets.ContainsKey(collider.name))
            AddTarget(collider.gameObject);
        Debug.Log("entered" + collider.name);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        RemoveTarget(collider.gameObject);
        Debug.Log("left" + collider.name);
    }

    public void AddTarget(GameObject gameObject)
    {
        targets.Add(gameObject.name, gameObject);
        if(gameObject.name != "Field")
            target = gameObject;
    }

    public void RemoveTarget(GameObject gameObject)
    {
        targets.Remove(gameObject.name);
        if (gameObject.name != "Field")
        {
            if (target.name == gameObject.name)
                target = null;
            foreach (GameObject t in targets.Values)
            {
                if (t.name != "Field")
                {
                    target = t;
                    break;
                }
            }
        }
    }

    public bool IsInField()
    {
        return targets.ContainsKey("Field");
    }
}
