using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHunt : MonoBehaviour
{
    private int timeBetweenHunts = 10;
    private int huntingTimer = 10;
    private float flickerTimer = 0.5f;
    private bool isGhostHunting;
    public bool isLightFlickering;
    private bool isInCrucifixRange;
    private Light flashlights;
    [SerializeField] private Light[] houseLights = new Light[14];
    private GameObject ghost;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Ghost");
        flashlights = GameObject.Find("FlashLight (Interactable)").GetComponent<Light>();

        houseLights = GameObject.Find("HouseLights").GetComponentsInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGhostHunting)
        {
            isGhostHunting = true;
            StartCoroutine(nameof(GhostHuntPrep));
        }
    }

    private IEnumerator GhostHuntPrep()
    {
        yield return new WaitForSeconds(timeBetweenHunts);

        if (isInCrucifixRange)
        {
            Destroy(GameObject.Find("Crucifix"));
            isGhostHunting = false;
            isInCrucifixRange = false;
            yield break;
        }

        isLightFlickering = true;
        for (int i = 0; i < 14; i++)
        {
            houseLights[i].enabled = false;
        }
        
        ghost.GetComponent<MeshRenderer>().enabled = true;
        ghost.GetComponent<BoxCollider>().enabled = true;
        
        StartCoroutine(nameof(GhostHuntTimer));

        yield return new WaitForSeconds(huntingTimer);
        
        isGhostHunting = false;
        
        ghost.GetComponent<MeshRenderer>().enabled = false;
        ghost.GetComponent<BoxCollider>().enabled = false;
        isLightFlickering = false;


    }

    private IEnumerator GhostHuntTimer()
    {
        while (isLightFlickering)
        {
            flashlights.enabled = true;
            yield return new WaitForSeconds(flickerTimer);
            flashlights.enabled = false;
            yield return new WaitForSeconds(flickerTimer);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name != "GhostKiller") return;

        isInCrucifixRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "GhostKiller") return;

        isInCrucifixRange = false;
    }
}

