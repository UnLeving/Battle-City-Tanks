using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Helpers
{
    public interface IPoolable<T>
        where T: MonoBehaviour
    {
        IObjectPool<T> ObjectPool { set; }
    }

    public class ObjectPooling<T> : MonoBehaviour
        where T: MonoBehaviour, IPoolable<T>
    {
        public static ObjectPooling<T> Instance { get; private set; }

        [SerializeField] private T prefab;
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 30;

        private IObjectPool<T> _objectPool;
        private List<T> _activeItem = new();

        public T Item => _objectPool.Get();

        private void Awake()
        {
            Instance = this;
            
            _objectPool = new ObjectPool<T>(Create,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
        }
        
        private T Create()
        {
            var item = Instantiate(prefab, transform);
            item.ObjectPool = _objectPool;

            _activeItem.Add(item);

            return item;
        }

        private void OnReleaseToPool(T pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        private void OnGetFromPool(T pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        private void OnDestroyPooledObject(T pooledObject)
        {
            _activeItem.Remove(pooledObject);

            Destroy(pooledObject.gameObject);
        }
    }
}