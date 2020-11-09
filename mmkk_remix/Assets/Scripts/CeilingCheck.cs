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
            if (CatController.me.jumpDelay < CatController.me.jumpDuration)
            {
                CatController.me.onCeiling = true;
            }
        }
    }
}
