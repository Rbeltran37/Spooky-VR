using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool IsOn;
    public Light LightSource;

    public void Flash()
    {
        if (GameObject.Find("Ghost").GetComponent<GhostHunt>().isLightFlickering == false)
        {
            if (IsOn == true)
            {
                LightSource.enabled = true;
                IsOn = false;
            }

            else
            {
                LightSource.enabled = false;
                IsOn = true;
            }
        }
    }
}
