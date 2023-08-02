using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    /// <summary>
    /// Event is triggered when open match list area
    /// </summary>
    public struct OpenMatchListEvent
    {
    }

    public class MatchController : Scroller
    {
        [SerializeField] private uint _matchLength = 5;

        private List<Match> _matchList = new List<Match>();

        protected override void Start()
        {
            _matchList = SharedLevelManager.Instance.GetRandomMatchList(_matchLength);
            amountToPool = (uint) _matchList.Count;
            base.Start();
            GetMatchValue();
            SetTeamPadding();
        }

        private void GetMatchValue()
        {
            if (scrollerElementList.Count > 0)
                ClearArea();

            if (amountToPool == 0)
            {
                amountToPool = (uint) _matchList.Count;
            }

            for (int i = 0; i < _matchList.Count; i++)
            {
                var element = GetPooledObject();
                MatchElement teamElement = (MatchElement) element;
                teamElement.SetTeamMember(_matchList[i]);
                element.gameObject.SetActive(true);
                scrollerElementList.Add(element);
            }
        }

        void ClearArea()
        {
            foreach (var element in scrollerElementList)
            {
                element.gameObject.SetActive(false);
            }

            scrollerElementList.Clear();
        }
    }
}