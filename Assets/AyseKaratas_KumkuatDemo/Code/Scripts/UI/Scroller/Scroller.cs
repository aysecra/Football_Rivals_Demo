using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KumkuatDemo
{
    public class Scroller : ObjectPool
    {
        [SerializeField] private Transform _teamMembersContainer;
        [SerializeField] private GameObject _characterButtonPrefab;
        [SerializeField] private Scrollbar _scrollbar;

        protected List<PoolableObject> scrollerElementList = new List<PoolableObject>();
        private bool _isBeginScrollbar;

        protected override void Start()
        {
            base.Start();
            
            for (int i = 0; i < amountToPool; i++)
            {
                var element =  GetPooledObject();
                element.gameObject.SetActive(true);
                scrollerElementList.Add(element);
            }
        }

        private void Update()
        {
            if (!_isBeginScrollbar && _scrollbar.value != 1)
            {
                _scrollbar.value = 1;
                _isBeginScrollbar = true;
            }
        }
    }
}