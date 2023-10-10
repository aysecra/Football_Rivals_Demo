using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FootballRivalsDemo
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
            // if (scrollerElementList.Count > 0)
            // {
            //     scrollerElementList = scrollerElementList.OrderBy(element => element.transform.position.y).ToList();
            //     
            //     int newTopPointValue = (int) (scrollerElementList[0].transform.position.y - _topPoint.position.y);
            //
            //     if (newTopPointValue < 0)
            //         _scrollerGridLayoutGroup.padding.top =
            //             scrollerElementList.Count > _maxScrollerElement ? 0 : newTopPointValue;
            // }
        }
    }
}