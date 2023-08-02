using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    public class GUIManager : Singleton<GUIManager>
        , EventListener<SelectTeamEvent>
        , EventListener<OpenTeamListEvent>
        , EventListener<OpenMatchListEvent>
    {
        [Header("Core Elements")] [SerializeField]
        private GameObject _teamArea;

        [SerializeField] private GameObject _teamMemberArea;
        [SerializeField] private GameObject _teamMatchArea;
        [SerializeField] private GameObject _teamAttackArea;

        [Header("Script Elements")] [SerializeField]
        private TeamController _teamAreaScript;

        [SerializeField] private TeamMemberController _teamMemberAreaScript;
        [SerializeField] private MatchController _teamMatchAreaScript;
        [SerializeField] private AttackUIController _teamAttackAreaScript;


        private void Start()
        {
            if (ProgressManager.Instance.GetCurrentTeam() == "")
            {
                OpenTeamArea();
            }
            else
            {
                OpenMatchArea();
            }
        }

        private void OpenTeamArea()
        {
            _teamArea.SetActive(true);
            _teamMemberArea.SetActive(false);
            _teamMatchArea.SetActive(false);
            _teamAttackArea.SetActive(false);
        }

        private void OpenTeamMemberArea()
        {
            _teamArea.SetActive(false);
            _teamMemberArea.SetActive(true);
            _teamMatchArea.SetActive(false);
            _teamAttackArea.SetActive(false);
        }

        private void OpenMatchArea()
        {
            _teamArea.SetActive(false);
            _teamMemberArea.SetActive(false);
            _teamMatchArea.SetActive(true);
            _teamAttackArea.SetActive(false);
        }

        private void OpenAttackArea()
        {
            _teamArea.SetActive(false);
            _teamMemberArea.SetActive(false);
            _teamMatchArea.SetActive(false);
            _teamAttackArea.SetActive(true);
        }

        private void OnEnable()
        {
            EventManager.EventStartListening<SelectTeamEvent>(this);
            EventManager.EventStartListening<OpenTeamListEvent>(this);
            EventManager.EventStartListening<OpenMatchListEvent>(this);
        }

        private void OnDisable()
        {
            EventManager.EventStopListening<SelectTeamEvent>(this);
            EventManager.EventStopListening<OpenTeamListEvent>(this);
            EventManager.EventStopListening<OpenMatchListEvent>(this);
        }

        public void OnEventTrigger(SelectTeamEvent currentEvent)
        {
            OpenTeamMemberArea();
            Team team = SharedLevelManager.Instance.GetTeam(currentEvent.Team);
            if (team != null)
                _teamMemberAreaScript.GetTeamMemberValues(team);
        }

        public void OnEventTrigger(OpenTeamListEvent currentEvent)
        {
            OpenTeamArea();
        }

        public void OnEventTrigger(OpenMatchListEvent currentEvent)
        {
            OpenMatchArea();
        }
    }
}