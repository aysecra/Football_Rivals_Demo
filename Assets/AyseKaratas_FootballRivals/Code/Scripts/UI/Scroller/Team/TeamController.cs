using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballRivalsDemo
{
    public class TeamController : Scroller
    {
        protected override void Start()
        {
            amountToPool = (uint) SharedLevelManager.Instance.TeamList.Count;
            base.Start();
            GetTeamValues();
            SetTeamPadding();
        }

        private void GetTeamValues()
        {
            List<Team> teamList = SharedLevelManager.Instance.TeamList;
            
            for (int i = 0; i < teamList.Count; i++)
            {
                var element = GetPooledObject();
                TeamElement teamElement = (TeamElement) element;
                teamElement.SetTeamElement(teamList[i]);
                element.gameObject.SetActive(true);
                scrollerElementList.Add(element);
            }
        }
    }
}

