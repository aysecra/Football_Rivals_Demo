using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FootballRivalsDemo
{
    /// <summary>
    /// Event is triggered when selecting specific match
    /// </summary>
    public struct SelectMatchEvent
    {
        public Match Match;

        public SelectMatchEvent(Match match)
        {
            Match = match;
        }
    }
    
    public class MatchElement : PoolableObject
    {
        [SerializeField] private Image _imgTeam1;
        [SerializeField] private Image _imgTeam2;
        [SerializeField] private TextMeshProUGUI _txtTeamName1;
        [SerializeField] private TextMeshProUGUI _txtTeamName2;
        [SerializeField] private TextMeshProUGUI _txtScore;

        private uint _score1 = 0;
        private uint _score2 = 0;
        private Match _match;

        public void SetTeamMember(Match match)
        {
            _match = match;
            _imgTeam1.sprite = match.Teams[0].Image;
            _imgTeam2.sprite = match.Teams[1].Image;
            _txtTeamName1.text = match.Teams[0].Title;
            _txtTeamName2.text = match.Teams[1].Title;
            _txtScore.text = $"{_score1} - {_score2}";
        }

        public void OnClickMatchButton()
        {
            EventManager.TriggerEvent(new SelectMatchEvent(_match));
        }
    }
}
