using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFixTask : MonoBehaviour
{
    private Animator doorAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        doorAnimation = transform.parent.GetComponent<Animator>();
    }

    public void DoorFix()
    {
        if (doorAnimation.GetCurrentAnimatorStateInfo(0).IsName("DoorStuckOpening") == true)
        {
            doorAnimation.SetBool("DoorOpening", true);
        }

        if (doorAnimation.GetCurrentAnimatorStateInfo(0).IsName("DoorStuckClosing") == true)
        {
            doorAnimation.SetBool("DoorClosing", true);
        }
    }
}
