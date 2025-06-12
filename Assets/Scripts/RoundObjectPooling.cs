using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RoundObjectPooling : MonoBehaviour
{
    public static RoundObjectPooling Instance { get; private set; }

    [SerializeField] private Round prefab;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 30;
    
    private IObjectPool<Round> _objectPool;
    private List<Round> _activeRounds = new();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _objectPool = new ObjectPool<Round>(Create,
            OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
    }
    
    private Round Create()
    {
        var tile = Instantiate(prefab, transform);
        tile.ObjectPool = _objectPool;
        
        _activeRounds.Add(tile);

        return tile;
    }

    private void OnReleaseToPool(Round pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Round pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Round pooledObject)
    {
        _activeRounds.Remove(pooledObject);
            
        Destroy(pooledObject.gameObject);
    }
}
