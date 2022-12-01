using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewParent : MonoBehaviour
{
    [SerializeField] private Transform newParent;
    [SerializeField] private Vector3 newLocalPosition;
    [SerializeField] private Vector3 newLocalEuler;
    [SerializeField] private CustomEnums.Execution execution;
    

    private void Awake()
    {
        if (execution != CustomEnums.Execution.Awake) return;
        
        Transform thisTransform;
        (thisTransform = transform).SetParent(newParent);
        thisTransform.localPosition = newLocalPosition;
        thisTransform.localEulerAngles = newLocalEuler;
    }

    private void OnEnable()
    {
        if (execution != CustomEnums.Execution.OnEnable) return;
        
        Transform thisTransform;
        (thisTransform = transform).SetParent(newParent);
        thisTransform.localPosition = newLocalPosition;
        thisTransform.localEulerAngles = newLocalEuler;
    }

    private void Start()
    {
        if (execution != CustomEnums.Execution.Start) return;
        
        Transform thisTransform;
        (thisTransform = transform).SetParent(newParent);
        thisTransform.localPosition = newLocalPosition;
        thisTransform.localEulerAngles = newLocalEuler;
    }
}
