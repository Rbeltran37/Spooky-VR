using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentCrawling : MonoBehaviour
{
    //private GameObject vent;
    public Material red;

    // Start is called before the first frame update
    void Start()
    {
        //vent = GameObject.Find("Air_Grid_01");
    }
    
    public void ChangeColor()
    {
        GetComponent<Renderer>().material = red;
        Debug.Log("Color changed?");
    }
}
