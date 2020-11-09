using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudFlash : MonoBehaviour
{
    private Coroutine c = null;
    private bool flashing = false;
    private Image img;
    public float flashFreq;
    
    void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        
    }

    // called from cat controller
    public void Flash(bool b)
    {
        if (!flashing && b)
        {
            c = StartCoroutine(HUDFlash());
        }
        else if (!b)
        {
            if (c != null)
            {
                StopCoroutine(c);
            }
            flashing = false;
        }
    }

    IEnumerator HUDFlash()
    {
        flashing = true;
        while (enabled)
        {
            img.enabled = !img.enabled;
            yield return new WaitForSeconds(flashFreq);
        }
    }
}
