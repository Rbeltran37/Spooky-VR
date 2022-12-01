using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] private Animator doorAnimation;
    [SerializeField] private bool doorTransition = false;
    [SerializeField] private int doorProbability;
    
    // Start is called before the first frame update
    void Start()
    {
        doorAnimation = transform.parent.GetComponent<Animator>();
    }

    public void DoorAnimation()
    {
        if (doorAnimation.GetBool("DoorOpening") == false && doorAnimation.GetBool("DoorClosing") == true && doorTransition == false)
        {
            doorProbability = Random.Range(1, 11);
            
            if (doorProbability == 10)
            {
                doorAnimation.SetBool("DoorClosing", false);
                doorAnimation.SetTrigger("DoorOpeningInterrupt");
            }
            else
            {
                doorAnimation.SetBool("DoorOpening", true);
                doorAnimation.SetBool("DoorClosing", false);
                doorTransition = true;
                StartCoroutine(DoorTransition());
            }
        }

        else if (doorAnimation.GetBool("DoorOpening") == true && doorAnimation.GetBool("DoorClosing") == false && doorTransition == false)
        {
            doorProbability = Random.Range(1, 11);

            if (doorProbability == 10)
            {
                doorAnimation.SetBool("DoorOpening", false);
                doorAnimation.SetTrigger("DoorClosingInterrupt");
            }
            else
            {
                doorAnimation.SetBool("DoorClosing", true);
                doorAnimation.SetBool("DoorOpening", false);
                doorTransition = true;
                StartCoroutine(DoorTransition());
            }
        }
    }
    
    IEnumerator DoorTransition()
    {
        yield return new WaitForSeconds(3);
        
        doorTransition = false;
    }

}
