using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledRigidbody : PooledObject
{
    [SerializeField] protected Rigidbody ThisRigidbody;

    private bool _resetIsKinematic;
    private bool _resetUseGravity;

    public Rigidbody GetRigidbody()
    {
        return ThisRigidbody;
    }

    protected override void Initialize()
    {
        base.Initialize();

        if (!ThisRigidbody)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisRigidbody)} is null. Should be set in editor. Attempting to get.", this);
            ThisRigidbody = GetComponent<Rigidbody>();
            if (!ThisRigidbody)
            {
                DebugLogger.Error(nameof(Initialize), $"{nameof(ThisRigidbody)} is null, was not found on {ThisGameObject.name}. Should be set in editor.", this);
                return;
            }
        }
        
        _resetIsKinematic = ThisRigidbody.isKinematic;
        _resetUseGravity = ThisRigidbody.useGravity;
    }

    protected override void ResetObject()
    {
        base.ResetObject();
        
        if (!ThisRigidbody)
        {
            DebugLogger.Error(nameof(ResetObject), $"{nameof(ThisRigidbody)} is null. Should be set in editor.", this);
            return;
        }
        
        ThisRigidbody.velocity = Vector3.zero;
        ThisRigidbody.angularVelocity = Vector3.zero;
        ThisRigidbody.isKinematic = _resetIsKinematic;
        ThisRigidbody.useGravity = _resetUseGravity;
    }
}
