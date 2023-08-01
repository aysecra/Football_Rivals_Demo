using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KumkuatDemo
{
    
    /// <summary>
    /// Event is triggered when selecting specific team
    /// </summary>
    public struct SelectTeamEvent
    {
        public Team Team;

        public SelectTeamEvent(Team team)
        {
            Team = team;
        }
    }
    
    public class TeamElement : PoolableObject
    {
        [SerializeField] private Image _imgTeam;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TextMeshProUGUI _txtStar;
        [SerializeField] private TextMeshProUGUI _txtTeamMember;

        private Team _currentTeam;

        public void SetTeamElement(Team team)
        {
            _currentTeam = team;
            _imgTeam.sprite = team.Image;
            _txtName.text = team.Title;
            _txtStar.text = team.Star.ToString();
            _txtTeamMember.text = $"{team.TeamMember}/{team.MaxTeamMember}";
        }

        public void OnButtonClick()
        {
            EventManager.TriggerEvent(new SelectTeamEvent(_currentTeam));
        }
        
    }
}