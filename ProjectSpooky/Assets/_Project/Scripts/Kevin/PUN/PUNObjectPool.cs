using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Networked/PUNObjectPool", order = 1)]
public class PUNObjectPool : ObjectPool
{
    private string _prefabName;

    private const float WAIT_INTERVAL = .1f;
    

    public override void InitializePool()
    {
        if (!objectPrefab)
        {
            DebugLogger.Error(nameof(InitializePool), $"objectPrefab is null. Must be set in editor.", this);
            return;
        }
        
        _prefabName = objectPrefab.name;
        
        if (!PhotonNetwork.OfflineMode && !PhotonNetwork.IsConnectedAndReady)
        {
            CoroutineCaller.Instance.StartCoroutine(InitializePoolCoroutine());
        }
        else
        {
            InitializePoolInstant();
        }
    }
    
    private IEnumerator InitializePoolCoroutine()
    {
        yield return WaitToConnect();
        
        InitializePoolInstant();
    }

    private IEnumerator WaitToConnect()
    {
        if (PhotonNetwork.OfflineMode) yield break;
        
        while (!PhotonNetwork.IsConnectedAndReady)
        {
            yield return new WaitForSeconds(WAIT_INTERVAL);
        }
    }
    
    private void InitializePoolInstant()
    {
        base.InitializePool();
    }
    
    protected override void InstantiateObject()
    {
        var pooledGameObject = PhotonNetwork.Instantiate(_prefabName, Vector3.zero, Quaternion.identity, 0);
        if (!pooledGameObject)
        {
            DebugLogger.Error(nameof(InstantiateObject), $"{nameof(pooledGameObject)} is null. _prefabName={_prefabName}", this);
            return;
        }

        var pooledObject = pooledGameObject.GetComponent<PooledObject>();
        if (!pooledObject)
        {
            DebugLogger.Error(nameof(InstantiateObject), $"{nameof(pooledObject)} is null.", this);
            return;
        }

        ObjectPoolStack.Push(pooledObject);
    }
}
