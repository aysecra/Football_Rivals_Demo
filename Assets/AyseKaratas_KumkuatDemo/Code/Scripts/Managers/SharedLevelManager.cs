using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    /// <summary>
    /// Event is triggered when team member must update
    /// </summary>
    public struct UpdateTeamMemberEvent
    {
    }

    public class SharedLevelManager : PersistentSingleton<SharedLevelManager>
    {
        [SerializeField] private List<Team> _teamList = new List<Team>();
        [SerializeField] private List<TeamMember> _teamMemberList = new List<TeamMember>();

        public List<Team> TeamList => _teamList;

        private TeamMember _player;

        public List<Match> GetRandomMatchList(uint Length)
        {
            return null;
        }

        public Attack GetRandomMatchMembers(Match match)
        {
            return null;
        }

        public List<TeamMember> GetTeamMembers(Team team)
        {
            List<TeamMember> members = new List<TeamMember>();
            foreach (TeamMember teamMember in _teamMemberList)
            {
                if (teamMember.Team == team.Title)
                {
                    members.Add(teamMember);
                }
            }

            return members;
        }

        public Team GetTeam(string teamName)
        {
            foreach (var team in _teamList)
            {
                if (teamName == team.Title)
                    return team;
            }

            return null;
        }

        public void AddPlayerToTeam(Team team, TeamMember teamMember)
        {
            if (_player == null)
            {
                _teamMemberList.Add(teamMember);
                _player = teamMember;
            }
            else
            {
                UpdatePlayerToTeam(team, teamMember);
            }

            EventManager.TriggerEvent(new UpdateTeamMemberEvent());
        }

        public void UpdatePlayerToTeam(Team team, TeamMember teamMember)
        {
            for (int i = _teamMemberList.Count - 1; i >= 0; i--)
            {
                if (_teamMemberList[i].Name == teamMember.Name)
                {
                    _teamMemberList[i].Team = team.Title;
                    break;
                }
            }

            EventManager.TriggerEvent(new UpdateTeamMemberEvent());
        }
    }
}