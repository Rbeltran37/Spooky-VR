using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemp : MonoBehaviour
{
    public int roomTemp;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in GameObject.Find("Ghost Rooms").transform)
        {
            if (child.gameObject.activeSelf)
            {
                if (gameObject.name == child.gameObject.name)
                {
                    roomTemp = Random.Range(33, 46);
                }

                else
                {
                    roomTemp = Random.Range(46, 76);
                }
            }
        }
    }
}
