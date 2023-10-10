using System.Collections;
using System.Collections.Generic;
using FootballRivalsDemo;
using TMPro;
using UnityEngine;

namespace FootballRivalsDemo
{
    /// <summary>
    /// Event is triggered when open match list area
    /// </summary>
    public struct OpenAttackListEvent
    {
    }

    public class AttackUIController : Scroller
        , EventListener<UpdateAttackListEvent>
    {
        [SerializeField] private TextMeshProUGUI _txtTitle;

        private Match _match;
        private Attack _attack;
        private bool _isStart = false;


        protected override void Start()
        {
            if (false)
                base.Start();
        }

        void ClearArea()
        {
            foreach (var element in scrollerElementList)
            {
                element.gameObject.SetActive(false);
            }

            scrollerElementList.Clear();
        }


        private void UpdateAttackListValues()
        {
            GetAttackListValues(_attack);
            SetTeamPadding();
        }

        public void GetAttackValues(Attack attack)
        {
            if (_match != attack.Match)
            {
                GetAttackListValues(attack);
            }

            SetTeamPadding();
        }

        private void GetAttackListValues(Attack attack)
        {
            _match = attack.Match;
            _attack = attack;

            ClearArea();

            _txtTitle.text = _match.Teams[0].Title + " - " + _match.Teams[1].Title;

            if (amountToPool == 0)
            {
                amountToPool = (uint) attack.Team1.Count;
                base.Start();
            }

            for (int i = 0; i < attack.Team1.Count; i++)
            {
                var element = GetPooledObject();
                AttackElement teamElement = (AttackElement) element;
                teamElement.SetAttackElement(attack.Team1[i], attack.Team2[i]);
                element.gameObject.SetActive(true);
                scrollerElementList.Add(element);
            }
        }

        public void OnClickBackButton()
        {
            EventManager.TriggerEvent(new OpenMatchListEvent());
        }

        private void OnEnable()
        {
            EventManager.EventStartListening<UpdateAttackListEvent>(this);
        }

        private void OnDisable()
        {
            EventManager.EventStopListening<UpdateAttackListEvent>(this);
        }

        public void OnEventTrigger(UpdateAttackListEvent currentEvent)
        {
            UpdateAttackListValues();
        }
    }
}