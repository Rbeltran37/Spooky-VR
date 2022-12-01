using System;
using System.Collections;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;

public class GrabbableObject : MonoBehaviour, IGrabbable
{
    [SerializeField] protected PhotonView ThisPhotonView;
    [SerializeField] protected Transform ThisTransform;
    [SerializeField] protected Rigidbody ThisRigidbody;

    protected Interactor CurrentInteractor;

    protected bool IsMine => ThisPhotonView && ThisPhotonView.IsMine;
    protected bool IsNotMine => ThisPhotonView && !ThisPhotonView.IsMine;


    protected virtual void Awake()
    {
        Initialize();
    }

    [Button]
    private void Initialize()
    {
        if (!ThisTransform)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisTransform)} is null. Should be set in editor. Attempting to set.", this);
            ThisTransform = transform;
            if (!ThisTransform)
            {
                DebugLogger.Error(nameof(Initialize), $"{nameof(ThisTransform)} is null. Should be set in editor. Was not able to set.", this);
                return;
            }
        }
        
        if (!ThisRigidbody)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisRigidbody)} is null. Should be set in editor. Attempting to set.", this);
            ThisRigidbody = ThisTransform.GetComponent<Rigidbody>();
            if (!ThisRigidbody)
            {
                DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisRigidbody)} is null. Should be set in editor. Was not able to set.", this);
                ThisRigidbody = gameObject.AddComponent<Rigidbody>();
            }
        }
        
        if (!ThisPhotonView)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisPhotonView)} is null. Should be set in editor. Attempting to set.", this);
            ThisPhotonView = gameObject.GetComponent<PhotonView>();
            if (!ThisPhotonView)
            {
                ThisPhotonView = gameObject.AddComponent<PhotonView>();
            }
        }
        
        ThisPhotonView.OwnershipTransfer = OwnershipOption.Takeover;

        var photonRigidBodyView = GetComponent<PhotonRigidbodyView>();
        if (!photonRigidBodyView)
        {
            photonRigidBodyView = gameObject.AddComponent<PhotonRigidbodyView>();
        }

        photonRigidBodyView.m_SynchronizeVelocity = true;
        photonRigidBodyView.m_SynchronizeAngularVelocity = true;
    }

    //Only local player calls
    public void AttemptGrab(Interactor interactor)
    {
        if (CurrentInteractor) AttemptUnGrab();

        TransferOwnership();
        Grab(interactor);
        SendGrab(interactor);
    }

    public virtual void Grab(Interactor interactor)
    {
        CurrentInteractor = interactor;
        CurrentInteractor.Attach(ThisRigidbody);
    }

    protected void SendGrab(Interactor interactor)
    {
        if (interactor == null)
        {
            DebugLogger.Error(nameof(SendGrab), $"{nameof(interactor)} is null.", this);
            return;
        }
        
        if (PhotonNetwork.OfflineMode) return;
        
        if (!ThisPhotonView)
        {
            DebugLogger.Error(nameof(SendGrab), $"{nameof(ThisPhotonView)} is null.", this);
            return;
        }

        var photonView = interactor.GetPhotonView();
        if (!photonView)
        {
            DebugLogger.Error(nameof(SendGrab), $"{nameof(photonView)} is null.", this);
            return;
        }
        
        var photonViewId = photonView.ViewID;
        ThisPhotonView.RPC(nameof(RPCGrab), RpcTarget.OthersBuffered, photonViewId);
    }

    [PunRPC]
    protected void RPCGrab(int photonViewId)
    {
        var interactorPhotonView = PhotonNetwork.GetPhotonView(photonViewId);
        if (!interactorPhotonView)
        {
            DebugLogger.Error(nameof(RPCGrab), $"{nameof(interactorPhotonView)} is null.", this);
            return;
        }

        var interactor = interactorPhotonView.GetComponent<Interactor>();
        if (!interactor)
        {
            DebugLogger.Error(nameof(RPCGrab), $"{nameof(interactor)} is null.", this);
            return;
        }
        
        Grab(interactor);
    }

    public void AttemptUnGrab()
    {
        if (!CurrentInteractor) return;
        
        UnGrab();
        SendUnGrab();
    }

    public virtual void UnGrab()
    {
        if (!CurrentInteractor) return;
        
        CurrentInteractor.Detach();
        
        CurrentInteractor = null;
    }

    protected void SendUnGrab()
    {
        if (PhotonNetwork.OfflineMode) return;

        ThisPhotonView.RPC(nameof(RPCUnGrab), RpcTarget.OthersBuffered);
    }

    [PunRPC]
    protected void RPCUnGrab()
    {
        UnGrab();
    }

    public bool IsGrabbed()
    {
        return CurrentInteractor != null;
    }

    public bool IsGrabbedBy(Interactor interactor)
    {
        return CurrentInteractor == interactor;
    }

    private void TransferOwnership()
    {
        if (PhotonNetwork.OfflineMode) return;
        if (IsMine) return;

        if (!ThisPhotonView)
        {
            DebugLogger.Error(nameof(TransferOwnership), $"{nameof(ThisPhotonView)} is null. Must be set in editor.", this);
            return;
        }

        ThisPhotonView.TransferOwnership(PhotonNetwork.LocalPlayer);
    }
}
