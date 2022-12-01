using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch2 : MonoBehaviour
{
    public bool IsOn;
    public Light LightSource;
    public Light LightSource2;

    public void Flash()
    {
        if (GameObject.Find("Ghost").GetComponent<GhostHunt>().isLightFlickering == false)
        {

            if (IsOn == true)
            {
                LightSource.enabled = true;
                LightSource2.enabled = true;
                IsOn = false;
            }

            else
            {
                LightSource.enabled = false;
                LightSource2.enabled = false;
                IsOn = true;
            }
        }
    }
}
