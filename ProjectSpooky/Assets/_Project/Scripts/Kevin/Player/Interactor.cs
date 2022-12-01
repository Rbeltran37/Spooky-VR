using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private PhotonView thisPhotonView;
    [SerializeField] private ConfigurableJoint attachConfigurableJoint;
    
    private IGrabbable _currentGrabbable;
    private IUsable _currentUsable;
    private bool _isGrabbing;


    private void OnTriggerEnter(Collider other)
    {
        CheckForGrabbableObjects(other);
        CheckForUsableObjects(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckForGrabbableObjects(other);
        CheckForUsableObjects(other);
    }

    private void OnTriggerExit(Collider other)
    {
        GrabExit(other);
        UseExit(other);
    }

    private void CheckForGrabbableObjects(Collider other)
    {
        if (_currentGrabbable != null) return;
        if (_isGrabbing) return;

        var grabbable = other.GetComponent<IGrabbable>();
        if (grabbable == null) return;
        
        _currentGrabbable = grabbable;
    }
    
    private void CheckForUsableObjects(Collider other)
    {
        if (_currentUsable != null) return;

        var usable = other.GetComponent<IUsable>();
        if (usable == null) return;

        _currentUsable = usable;
    }
    
    private void GrabExit(Collider other)
    {
        if (_isGrabbing) return;

        var grabbable = other.GetComponent<IGrabbable>();
        if (grabbable == null) return;

        _currentGrabbable = null;
    }
    
    private void UseExit(Collider other)
    {
        var usable = other.GetComponent<IUsable>();
        if (usable == null) return;

        _currentUsable = null;
    }

    public void AttemptGrab()
    {
        _isGrabbing = true;

        _currentGrabbable?.AttemptGrab(this);
    }

    public void AttemptUnGrab()
    {
        _isGrabbing = false;
        if (_currentGrabbable == null) return;
        
        if (_currentGrabbable.IsGrabbedBy(this))
        {
            _currentGrabbable.AttemptUnGrab();
        }

        _currentGrabbable = null;
    }

    public void AttemptUse()
    {
        _currentUsable?.AttemptUse(this);
    }

    public PhotonView GetPhotonView()
    {
        return thisPhotonView;
    }

    public void Attach(Rigidbody rigidbodyToConnect)
    {
        attachConfigurableJoint.connectedBody = rigidbodyToConnect;
        attachConfigurableJoint.autoConfigureConnectedAnchor = true;
    }

    public void Detach()
    {
        attachConfigurableJoint.connectedBody = null;
        attachConfigurableJoint.autoConfigureConnectedAnchor = false;
    }
}
