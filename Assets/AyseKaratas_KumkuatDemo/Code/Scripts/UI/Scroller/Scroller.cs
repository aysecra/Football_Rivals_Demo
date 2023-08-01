using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace KumkuatDemo
{
    public class Scroller : ObjectPool
    {
        [SerializeField] private GridLayoutGroup _scrollerGridLayoutGroup;
        [SerializeField] private Transform _scrollerElementContainer;
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private uint _maxScrollerElement;
        [SerializeField] private Transform _topPoint;

        protected List<PoolableObject> scrollerElementList = new List<PoolableObject>();
        private bool _isBeginScrollbar;

        private void Update()
        {
            if (_scrollerElementContainer.childCount > _maxScrollerElement)
            {
                if (!_isBeginScrollbar && _scrollbar.value != 1)
                {
                    _scrollbar.value = 1;
                    _isBeginScrollbar = true;
                }
            }
        }
        
        protected virtual void SetTeamPadding()
        {
            if(scrollerElementList.Count>0)
            {
                int newTopPointValue = (int) (scrollerElementList[0].transform.position.y - _topPoint.position.y);

                _scrollerGridLayoutGroup.padding.top =
                    _scrollerElementContainer.childCount > _maxScrollerElement ? 0 : newTopPointValue;
            }
        }
    }
}