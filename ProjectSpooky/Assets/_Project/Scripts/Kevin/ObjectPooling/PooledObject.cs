using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PooledObject : MonoBehaviour, IPoolable
{
    [SerializeField] protected PhotonView ThisPhotonView;
    [SerializeField] protected GameObject ThisGameObject;
    [SerializeField] protected Transform ThisTransform;
    
    [SerializeField] private float lifetime = IMMORTAL;

    private Coroutine _despawnCoroutine;
    private Transform _poolParent;
    private string _poolParentName;

    public Action<PooledObject> WasDespawned;

    private const string POOL_PARENT = "[_PooledObjects]";
    private const string CLONE = " (Clone)";
    private const string MINE = " MINE";
    private const float IMMORTAL = -1;
    private const float DELAY_TIME = .05f;


    protected virtual void Awake()
    {
        Initialize();
        ChildToPoolParent();
    }

    private void OnDestroy()
    {
        DebugLogger.Info(nameof(OnDestroy), $"{name} has been destroyed.", this);
    }

    public GameObject GetGameObject()
    {
        return ThisGameObject;
    }

    public Transform GetTransform()
    {
        return ThisTransform;
    }

    public void Spawn(Transform parent, Vector3 position, Quaternion rotation)
    {
        ThisTransform.SetParent(parent);
        
        Spawn(position, rotation);
    }
    
    public void Spawn(Vector3 position, Quaternion rotation)
    {
        ThisTransform.position = position;
        ThisTransform.rotation = rotation;

        Spawn();
    }
    
    public void Spawn()
    {
        ThisGameObject.SetActive(true);
        Despawn(lifetime);
        
        SendRPCSpawn();
    }

    private void SendRPCSpawn()
    {
        if (PhotonNetwork.OfflineMode) return;

        if (!ThisPhotonView) return;
        
        if (!ThisPhotonView.IsMine) return;

        ThisPhotonView.RPC(nameof(RPCSpawn), RpcTarget.OthersBuffered, ThisTransform.position, ThisTransform.rotation);
    }

    [PunRPC]
    protected void RPCSpawn(Vector3 position, Quaternion rotation)
    {
        Spawn(position, rotation);
    }

    public void Despawn(float delayTime)
    {
        if (_despawnCoroutine != null) StopCoroutine(_despawnCoroutine);

        if (delayTime.Equals(IMMORTAL)) return;

        _despawnCoroutine = StartCoroutine(DespawnDelayed(delayTime));
    }
    
    private IEnumerator DespawnDelayed(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        Despawn();
    }
    
    public void Despawn()
    {
        if (_despawnCoroutine != null) StopCoroutine(_despawnCoroutine);
        
        ThisGameObject.SetActive(false);
        ResetObject();
        
        WasDespawned?.Invoke(this);
        WasDespawned = null;
        
        SendRPCDespawn();
    }

    private void SendRPCDespawn()
    {
        if (PhotonNetwork.OfflineMode) return;

        if (!ThisPhotonView) return;
        
        if (!ThisPhotonView.IsMine) return;

        ThisPhotonView.RPC(nameof(RPCDespawn), RpcTarget.OthersBuffered);
    }

    [PunRPC]
    protected void RPCDespawn()
    {
        Despawn();
    }

    protected virtual void Initialize()
    {
        if (!ThisGameObject)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisGameObject)} is null. Should be set in editor.", this);
            ThisGameObject = gameObject;
        }
        
        if (!ThisTransform)
        {
            DebugLogger.Warning(nameof(Initialize), $"{nameof(ThisTransform)} is null. Should be set in editor.", this);
            ThisTransform = transform;
        }

        var prefabName = ThisGameObject.name.Substring(0, ThisGameObject.name.Length - (CLONE.Length - 1));
        _poolParentName = $"{POOL_PARENT} {prefabName}";

        if (ThisPhotonView)
        {
            _poolParentName += $" ({ThisPhotonView.Owner})";
            if (ThisPhotonView.IsMine)
            {
                _poolParentName += MINE;
            }
        }

        ThisGameObject.SetActive(false);
        //DontDestroyOnLoad(ThisGameObject);        //Persists through scenes
    }

    private void ChildToPoolParent()
    {
        if (!_poolParent)
        {
            var poolParentGameObject = GameObject.Find(_poolParentName);
            if (!poolParentGameObject)
            {
                poolParentGameObject = new GameObject(_poolParentName);
                //DontDestroyOnLoad(poolParentGameObject);        //Persists through scenes
            }

            _poolParent = poolParentGameObject.transform;
        }
        
        ThisTransform.SetParent(_poolParent);
    }

    protected virtual void ResetObject()
    {
        ChildToPoolParent();
    }
}
