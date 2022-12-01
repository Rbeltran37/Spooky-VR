using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucifixInteraction : MonoBehaviour
{
    private int ghostDisappearTimer = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "GhostKiller") return;

        StartCoroutine(nameof(Disappear));
    }

    private IEnumerator Disappear()
    {
        transform.Translate(-1,1,0);
        
        yield return new WaitForSeconds(ghostDisappearTimer);

        GetComponent<MeshRenderer>().enabled = false;
    }
}
