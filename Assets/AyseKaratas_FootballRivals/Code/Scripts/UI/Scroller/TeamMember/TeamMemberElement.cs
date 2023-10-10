using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FootballRivalsDemo
{
    public class TeamMemberElement : PoolableObject
    {
        [SerializeField] private Image _imgMember;
        [SerializeField] private TextMeshProUGUI _txtName;
        [SerializeField] private TextMeshProUGUI _txtStar;
        [SerializeField] private TextMeshProUGUI _txtMemberLevel;
        
        public void SetTeamMember(TeamMember teamMember)
        {
            _imgMember.sprite = teamMember.Image;
            _txtName.text = teamMember.Name;
            _txtStar.text = teamMember.Star.ToString();
            _txtMemberLevel.text = teamMember.Level.ToString();
        }
    }
}
