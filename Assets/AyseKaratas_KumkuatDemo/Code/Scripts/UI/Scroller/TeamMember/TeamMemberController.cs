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
    {
        [SerializeField] private TextMeshProUGUI _txtTitle;

        void ClearArea()
        {
            foreach (var element in scrollerElementList)
            {
                element.gameObject.SetActive(false);
            }
        }

        public void GetTeamMemberValues(Team team)
        {
            ClearArea();

            _txtTitle.text = team.Title;
            List<TeamMember> teamList = SharedLevelManager.Instance.GetTeamMembers(team);
            
            if (scrollerElementList.Count == 0)
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
    }
}