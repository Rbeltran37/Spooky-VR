using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF : MonoBehaviour
{
    public Material level1;
    public Material level2;
    public Material level3;
    public Material level4;
    public Material level5;

    public int emfLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (emfLevel == 1)
        {
            level1.EnableKeyword("_EMISSION");
        }

        if (emfLevel == 2)
        {
            level1.EnableKeyword("_EMISSION");
            level2.EnableKeyword("_EMISSION");
        }

        if (emfLevel == 3)
        {
            level1.EnableKeyword("_EMISSION");
            level2.EnableKeyword("_EMISSION");
            level3.EnableKeyword("_EMISSION");
        }

        if (emfLevel == 4)
        {
            level1.EnableKeyword("_EMISSION");
            level2.EnableKeyword("_EMISSION");
            level3.EnableKeyword("_EMISSION");
            level4.EnableKeyword("_EMISSION");
        }

        if (emfLevel == 5)
        {
            level1.EnableKeyword("_EMISSION");
            level2.EnableKeyword("_EMISSION");
            level3.EnableKeyword("_EMISSION");
            level4.EnableKeyword("_EMISSION");
            level5.EnableKeyword("_EMISSION");
        }

        if (emfLevel == 0)
        {
            level1.DisableKeyword("_EMISSION");
            level2.DisableKeyword("_EMISSION");
            level3.DisableKeyword("_EMISSION");
            level4.DisableKeyword("_EMISSION");
            level5.DisableKeyword("_EMISSION");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Ghost Room")
        {
            emfLevel = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ghost Room")
        {
            emfLevel = 0;
        }
    }
}
