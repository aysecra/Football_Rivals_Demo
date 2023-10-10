using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballRivalsDemo
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
            
            for(int i = 0; i < _amountToPool; i++)
            {
                AddNewObject();
            }
        }

        private void AddNewObject()
        {
            GameObject newObject = Instantiate(_poolableObject.gameObject, _parentObject);
            newObject.SetActive(_isObjectActive);
            PoolableObject newPoolableObject = newObject.GetComponent<PoolableObject>();
            _pooledObjects.Add(newPoolableObject);
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
            
            AddNewObject();
            return _pooledObjects[^1];
        }
    }
}