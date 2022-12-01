using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Core/ObjectPool", order = 1)]
public class ObjectPool : ScriptableObject
{
    [SerializeField] protected GameObject objectPrefab;
    [SerializeField] protected int poolSize = 10;

    protected Stack<PooledObject> ObjectPoolStack;

    public Action PoolWasFilled;

    protected const int EMPTY = 0;
    
    
    public virtual void InitializePool()
    {
        if (ObjectPoolStack != null && ObjectPoolStack.Count > EMPTY && ObjectPoolStack.Peek()) return;
        
        ObjectPoolStack = new Stack<PooledObject>(poolSize);
        FillPool();
    }

    private void FillPool()
    {
        for (var index = 0; index < poolSize; index++)
        {
            InstantiateObject();
        }
        
        PoolWasFilled?.Invoke();
    }
    
    protected virtual void InstantiateObject()
    {
        if (!objectPrefab)
        {
            DebugLogger.Error(nameof(InstantiateObject), $"{objectPrefab} is null. Must be set in editor.", this);
            return;
        }
        
        var pooledGameObject = Instantiate(objectPrefab);
        var pooledObject = pooledGameObject.GetComponent<PooledObject>();
        if (!pooledObject)
        {
            DebugLogger.Error(nameof(InstantiateObject), $"{nameof(pooledObject)} is null.", this);
            return;
        }

        ObjectPoolStack.Push(pooledObject);
    }
    
    public PooledObject GetPooledObject()
    {
        if (ObjectPoolStack == null) InitializePool();
        if (ObjectPoolStack.Count <= EMPTY) InstantiateObject();
        
        if (ObjectPoolStack.Count <= EMPTY)
        {
            DebugLogger.Error(nameof(GetPooledObject), $"{nameof(ObjectPoolStack)} is empty. Unable to Instantiate Objects.", this);
            return null;
        }

        var pooledObject = ObjectPoolStack.Pop();
        pooledObject.WasDespawned += ObjectPoolStack.Push;
        return pooledObject;
    }
}
