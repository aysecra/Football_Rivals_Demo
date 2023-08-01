using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform _parentObject;
        [SerializeField] private PoolableObject _poolableObject;
        [SerializeField] private uint _amountToPool;
        [SerializeField] private bool _isObjectActive;
        
        private List<PoolableObject> _pooledObjects;

        protected uint amountToPool
        {
            get => _amountToPool;
            set => _amountToPool = value;
        }

        protected virtual void Start()
        {
            _pooledObjects = new List<PoolableObject>();
            GameObject newObject;
            
            for(int i = 0; i < _amountToPool; i++)
            {
                newObject = Instantiate(_poolableObject.gameObject, _parentObject);
                newObject.SetActive(_isObjectActive);
                _pooledObjects.Add(newObject.GetComponent<PoolableObject>());
            }
        }
        
        public PoolableObject GetPooledObject()
        {
            for(int i = 0; i < _amountToPool; i++)
            {
                if(!_pooledObjects[i].gameObject.activeInHierarchy)
                {
                    return _pooledObjects[i];
                }
            }
            return null;
        }
    }
}