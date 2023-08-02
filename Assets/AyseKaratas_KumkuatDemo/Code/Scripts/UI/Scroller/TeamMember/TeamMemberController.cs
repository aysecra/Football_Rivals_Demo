using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KumkuatDemo
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


        private void UpdateTeamMemberValues()
        {
            GetMemberValues(_currentTeam);
            SetTeamPadding();
        }

        public void GetTeamMemberValues(Team team)
        {
            if (ProgressManager.Instance.GetCurrentTeam() == team.Title)
            {
                _btnJoinObject.SetActive(false);
            }
            else
            {
                _btnJoinObject.SetActive(true);
            }


            if (_currentTeam != team)
            {
                GetMemberValues(team);
            }

            SetTeamPadding();
        }

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
            EventManager.TriggerEvent(new OpenTeamListEvent());
        }

        public void OnClickJoinButton()
        {
            _btnJoinObject.SetActive(false);
            ProgressManager.Instance.SetCurrentTeam(_currentTeam);
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