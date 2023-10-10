using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FootballRivalsDemo
{
    public class AttackElement : PoolableObject
    {
        [SerializeField] private Image _imgTeam1;
        [SerializeField] private Image _imgTeam2;
        [SerializeField] private TextMeshProUGUI _txtTeamName1;
        [SerializeField] private TextMeshProUGUI _txtTeamName2;
        [SerializeField] private GameObject _btnAttack;
        [SerializeField] private GameObject _btnAssist;

        private uint _score1 = 0;
        private uint _score2 = 0;

        // set player attacking each other
        public void SetAttackElement(TeamMember team1, TeamMember team2)
        {
            _imgTeam1.sprite = team1.Image;
            _imgTeam2.sprite = team2.Image;
            _txtTeamName1.text = team1.Name;
            _txtTeamName2.text = team2.Name;

            if (team1.Name == ProgressManager.Instance.PlayerName)
            {
                _btnAttack.SetActive(true);
                _btnAssist.SetActive(false);
            }
            else
            {
                _btnAttack.SetActive(false);
                _btnAssist.SetActive(true);
            }
        }
    }
}