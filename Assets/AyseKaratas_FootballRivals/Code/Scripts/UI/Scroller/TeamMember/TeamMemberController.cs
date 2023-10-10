using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FootballRivalsDemo
{
    /// <summary>
    /// Event is triggered when open team list area
    /// </summary>
    public struct OpenTeamListEvent
    {
    }

    public class TeamMemberController : Scroller
                                        ,EventListener<UpdateTeamMemberEvent>
    {
        [SerializeField] private TextMeshProUGUI _txtTitle;
        [SerializeField] private GameObject _btnJoinObject;
        [SerializeField] private GameObject _btnTransferObject;

        private Team _currentTeam;
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

        // change elements of team member list area
        private void UpdateTeamMemberValues()
        {
            GetMemberValues(_currentTeam);
            SetTeamPadding();
        }

        // get all value according to user's team or not
        public void GetTeamMemberValues(Team team)
        {
            if (ProgressManager.Instance.GetCurrentTeam() == team.Title)
            {
                _btnJoinObject.SetActive(false);
                _btnTransferObject.SetActive(true);
            }
            else
            {
                _btnJoinObject.SetActive(true);
                _btnTransferObject.SetActive(false);
            }


            if (_currentTeam != team)
            {
                GetMemberValues(team);
            }

            SetTeamPadding();
        }

        // get team members that need to be shown
        private void GetMemberValues(Team team)
        {
            _currentTeam = team;

            ClearArea();

            _txtTitle.text = team.Title;
            List<TeamMember> teamList = SharedLevelManager.Instance.GetTeamMembers(team);

            if (amountToPool == 0)
            {
                amountToPool = (uint) teamList.Count;
                base.Start();
            }

            for (int i = 0; i < teamList.Count; i++)
            {
                var element = GetPooledObject();
                TeamMemberElement teamElement = (TeamMemberElement) element;
                teamElement.SetTeamMember(teamList[i]);
                element.gameObject.SetActive(true);
                scrollerElementList.Add(element);
            }
        }

        public void OnClickBackButton()
        {
            if (ProgressManager.Instance.GetCurrentTeam() == "")
            {
                EventManager.TriggerEvent(new OpenTeamListEvent());
            }
            else
            {
                EventManager.TriggerEvent(new OpenMatchListEvent());
            }
        }

        public void OnClickJoinButton()
        {
            _btnJoinObject.SetActive(false);
            _btnTransferObject.SetActive(true);
            ProgressManager.Instance.SetCurrentTeam(_currentTeam);
        }

        public void OnClickTransferButton()
        {
            EventManager.TriggerEvent(new OpenTeamListEvent());
        }

        private void OnEnable()
        {
            EventManager.EventStartListening<UpdateTeamMemberEvent>(this);
        }

        private void OnDisable()
        {
            EventManager.EventStopListening<UpdateTeamMemberEvent>(this);

        }

        public void OnEventTrigger(UpdateTeamMemberEvent currentEvent)
        {
            UpdateTeamMemberValues();
        }
    }
}