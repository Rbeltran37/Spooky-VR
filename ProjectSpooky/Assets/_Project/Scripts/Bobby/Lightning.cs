using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Light LightSource;
    public float timer;
    public AudioSource Jump;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TestCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(5);
        Jump.Play();
        LightSource.enabled = true;
        yield return new WaitForSeconds(.3f);
        LightSource.enabled = false;
        //yield break;
    }
}
