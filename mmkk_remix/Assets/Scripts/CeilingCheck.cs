using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    BoxCollider bc;
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Attack" && col.gameObject.tag != "Rat")
        {
            CatController.me.onCeiling = true;
        }
    }
}
