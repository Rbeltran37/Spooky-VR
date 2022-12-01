using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucifix : MonoBehaviour
{
    [SerializeField] private Material fire;

    private int crucifixDestroyTimer = 4;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Ghost") return;

        StartCoroutine(nameof(DestroyCrucifix));
    }

    private IEnumerator DestroyCrucifix()
    {
        transform.Find("GhostKiller").GetComponent<BoxCollider>().enabled = false;
        transform.Find("Cube1").GetComponent<MeshRenderer>().material = fire;
        transform.Find("Cube2").GetComponent<MeshRenderer>().material = fire;
        
        yield return new WaitForSeconds(crucifixDestroyTimer);
        
        Destroy(gameObject);
    }
}