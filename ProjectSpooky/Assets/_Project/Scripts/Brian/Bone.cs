using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    [SerializeField] private bool wasGrabbed;
    
    public void BoneWasGrabbed()
    {
        GetComponent<MeshRenderer>().enabled = false;
        wasGrabbed = true;
    }
}
