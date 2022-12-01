using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject[] ghostRoom = new GameObject[0];
    private string[] boneSpawnLocations = new string[77];
    [SerializeField] private string boneSpawnPoint;
    [SerializeField] private string dollSpawnPoint;
    public int dollForceSpawn;
    private GameObject ghostRooms;
    private GameObject ghost;
    private GameObject roomTemperatures;
    private GameObject bone;
    private GameObject doll;

    // Start is called before the first frame update
    private void Start()
    {
        ghostRoom[Random.Range(0, 6)].SetActive(true);

        ghost = GameObject.Find("Ghost");
        ghostRooms = GameObject.Find("Ghost Rooms");
        roomTemperatures = GameObject.Find("RoomTemperatures");
        bone = GameObject.Find("Bone");
        doll = GameObject.Find("HorrorDoll");

        foreach (Transform child in ghostRooms.transform)
        {
            if (child.gameObject.activeSelf)
            {
                ghost.transform.position = child.transform.GetChild(0).position;

            }
        }

        foreach (Transform child in roomTemperatures.transform)
        {
            child.gameObject.GetComponent<RoomTemp>().enabled = true;
        }

        for (int i = 0; i < 77; i++)
        {
            boneSpawnLocations[i] = GameObject.Find("Bone Spawn Locations").transform.GetChild(i).name;
        }
        boneSpawnPoint = boneSpawnLocations[Random.Range(0,76)];
        dollSpawnPoint = boneSpawnLocations[Random.Range(0,76)];
        if (dollSpawnPoint == boneSpawnPoint)
        {
            while (dollSpawnPoint == boneSpawnPoint)
            {
                dollSpawnPoint = boneSpawnLocations[Random.Range(0,76)];
            }
        }
        //boneSpawnPoint = boneSpawnLocations[0];     //force a spawn
        //dollSpawnPoint = boneSpawnLocations[dollForceSpawn];     //force a spawn
        
        foreach (Transform child in GameObject.Find("Bone Spawn Locations").transform)
        {
            if (boneSpawnPoint == child.gameObject.name)
            {
                bone.transform.position = child.transform.position;
                bone.transform.rotation = child.transform.rotation;
                if (child.gameObject.CompareTag("Drawer"))
                {
                    boneSpawnPoint = boneSpawnPoint.Substring(0, boneSpawnPoint.Length - 5);
                    boneSpawnPoint += "Collider";
                    bone.transform.parent = GameObject.Find(boneSpawnPoint).transform;
                }
                else
                {
                    bone.transform.parent = child.transform;
                }
            }
        }

        foreach (Transform child in GameObject.Find("Doll Spawn Locations").transform)
        {
            if (dollSpawnPoint == child.gameObject.name)
            {
                doll.transform.position = child.transform.position;
                doll.transform.rotation = child.transform.rotation;
                if (child.gameObject.CompareTag("Drawer"))
                {
                    dollSpawnPoint = dollSpawnPoint.Substring(0, dollSpawnPoint.Length - 5);
                    dollSpawnPoint += "Collider";
                    doll.transform.parent = GameObject.Find(dollSpawnPoint).transform;
                }
                else
                {
                    doll.transform.parent = child.transform;
                }
            }
        }

    }
}