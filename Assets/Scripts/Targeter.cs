using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour {

    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(target && target.gameObject.name == collision.gameObject.name)
        {
            target = null;
        }
    }
}
