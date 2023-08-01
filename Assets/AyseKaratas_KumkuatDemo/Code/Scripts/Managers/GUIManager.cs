using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    public class GUIManager : Singleton<GUIManager>
    {
        [SerializeField] private GameObject _teamArea;
        [SerializeField] private GameObject _teamMemberArea;
        [SerializeField] private GameObject _teamMatchArea;
        [SerializeField] private GameObject _teamAttackArea;


        private void Start()
        {
            OpenTeamArea();
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
            _teamMemberArea.SetActive(true);
            _teamMatchArea.SetActive(false);
            _teamAttackArea.SetActive(false);
        }

        private void OpenAttackArea()
        {
            _teamArea.SetActive(false);
            _teamMemberArea.SetActive(false);
            _teamMatchArea.SetActive(false);
            _teamAttackArea.SetActive(true);
        }
    }
}