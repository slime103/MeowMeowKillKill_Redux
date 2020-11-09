using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    BoxCollider bc;
    void Start()
    {
        bc = GetComponent<BoxCollider>();
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag != "Player" && col.gameObject.tag != "Attack" && col.gameObject.tag != "Rat")
        {
            if (CatController.me.jumpDelay < CatController.me.jumpDuration)
            {
                CatController.me.onGround = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Attack" && col.gameObject.tag != "Rat")
        {
            CatController.me.onGround = false;
        }
    }
}
