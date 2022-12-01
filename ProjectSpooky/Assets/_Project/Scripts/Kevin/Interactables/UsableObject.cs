using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class UsableObject : MonoBehaviour, IUsable
{
    [SerializeField] protected PhotonView ThisPhotonView;
    
    public UnityEvent WasUsed;

    
    protected virtual void Awake()
    {
        Initialize();
    }

    [Button]
    private void Initialize()
    {
        if (!ThisPhotonView)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisPhotonView)} is null. Should be set in editor. Attempting to set.", this);
            ThisPhotonView = gameObject.AddComponent<PhotonView>();
        }
        
        ThisPhotonView.OwnershipTransfer = OwnershipOption.Takeover;
    }

    //Only local player calls
    public void AttemptUse(Interactor interactor)
    {
        Use();
        SendUse();
    }

    public void Use()
    {
        WasUsed?.Invoke();
    }

    protected void SendUse()
    {
        if (PhotonNetwork.OfflineMode) return;
        
        if (!ThisPhotonView)
        {
            DebugLogger.Error(nameof(SendUse), $"{nameof(ThisPhotonView)} is null. Must be set in editor.", this);
            return;
        }
        
        ThisPhotonView.RPC(nameof(RPCUse), RpcTarget.OthersBuffered);
    }

    [PunRPC]
    protected void RPCUse()
    {
        Use();
    }
}
