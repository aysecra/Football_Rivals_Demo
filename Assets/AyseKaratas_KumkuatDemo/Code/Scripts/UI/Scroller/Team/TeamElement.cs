using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KumkuatDemo
{
    public class TeamElement : PoolableObject
    {
        [SerializeField] private Image _imgTeam;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TextMeshProUGUI _txtStar;
        [SerializeField] private TextMeshProUGUI _txtTeamMember;

        public void SetTeamElement(Team team)
        {
            _imgTeam.sprite = team.Image;
            _txtName.text = team.Title;
            _txtStar.text = team.Star.ToString();
            _txtTeamMember.text = $"{team.TeamMember}/{team.MaxTeamMember}";
        }
    }
}