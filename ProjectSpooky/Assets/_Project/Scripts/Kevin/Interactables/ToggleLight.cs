using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : OnOffToggleSwitch
{
    [SerializeField] private Light lightToToggle;


    protected override void Initialize()
    {
        if (!lightToToggle)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(lightToToggle)} is null. Should be set in editor. Attempting to find.", this);
            lightToToggle = GetComponentInChildren<Light>();
            if (!lightToToggle)
            {
                DebugLogger.Error(nameof(Initialize), $"{nameof(lightToToggle)} is null. Should be set in editor. Was not found.", this);
                return;
            }
        }
        
        SetIsOn(lightToToggle.enabled);
    }

    public void ActivateToggle()
    {
        if (!lightToToggle)
        {
            DebugLogger.Error(nameof(ActivateToggle), $"{nameof(lightToToggle)} is null. Should be set in editor.", this);
            return;
        }

        if (GameObject.Find("Ghost").GetComponent<GhostHunt>().isLightFlickering == false)
        {
            lightToToggle.enabled = Toggle();
        }
    }
}
