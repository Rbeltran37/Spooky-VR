using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThermometerBrian : MonoBehaviour
{
    private bool isOn;
    private bool isRoutineRunning;
    private int thermometerRefreshTimer = 1;
    private int goalTemp;
    private int currentTemp = 75;
    private TextMeshPro temp;

    private void Start()
    {
        temp = GameObject.Find("NormalRoomReading").GetComponent<TextMeshPro>();
    }

    public void ToggleThermometer()
    {
        if (!isOn)
        {
            isOn = true;
            transform.Find("NormalRoomReading").GetComponent<MeshRenderer>().enabled = true;
        }

        else
        {
            isOn = false;
            transform.Find("NormalRoomReading").GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isOn) return;

        if (other.gameObject.transform.parent.name == "RoomTemperatures")
        {
            if (!isRoutineRunning)
            {
                goalTemp = other.gameObject.GetComponent<RoomTemp>().roomTemp;
                StartCoroutine(nameof(UpdateThermometer));
            }
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isOn) return;

        if (other.gameObject.transform.parent.name == "RoomTemperatures")
        {
            if (isRoutineRunning)
            {
                StopCoroutine(nameof(UpdateThermometer));
                isRoutineRunning = false;
            }
        }
    }

    private IEnumerator UpdateThermometer()
    {
        isRoutineRunning = true;
        
        while (currentTemp != goalTemp && isOn)
        {
            yield return new WaitForSeconds(thermometerRefreshTimer);
            
            if (currentTemp > goalTemp)
            {
                currentTemp--;
                temp.SetText(currentTemp.ToString());
            }

            if (currentTemp < goalTemp)
            {
                currentTemp++;
                temp.SetText(currentTemp.ToString());
            }
            
        }

        isRoutineRunning = false;
    }
}