using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    private GameObject _doorCollider;
    private GameObject _door;
    private string _doorName;
    
    // Start is called before the first frame update
    void Start()
    {
        _doorCollider = this.gameObject;
        _doorName = _doorCollider.name.Substring(0,_doorCollider.name.Length - 8);
        _door = GameObject.Find(_doorName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _doorCollider.transform.position = _door.transform.position;
        _doorCollider.transform.rotation = _door.transform.rotation;
    }
}
