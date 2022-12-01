using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWritingBook : MonoBehaviour
{
    private int timeUntilGhostWriting = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.name == "Ghost Rooms")
        {
            StartCoroutine(nameof(GhostWriting));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.name == "Ghost Rooms")
        {
            StopCoroutine(nameof(GhostWriting));
        }
    }

    private IEnumerator GhostWriting()
    {
        yield return new WaitForSeconds(timeUntilGhostWriting);
        
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
