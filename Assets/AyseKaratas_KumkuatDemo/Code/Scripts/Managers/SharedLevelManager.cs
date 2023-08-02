using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public List<Match> GetRandomMatchList(uint length)
        {
            length = _teamList.Count > length ? length : (uint) (_teamList.Count - 2);
            string currTeam = ProgressManager.Instance.GetCurrentTeam();
            if (currTeam != "")
            {
                List<Match> matchList = new List<Match>();
                List<Team> tempList =_teamList;
                Team currTeamScript = GetTeam(currTeam);
                tempList.Remove(currTeamScript);

                for (int i = 0; i < length; i++)
                {
                    int randomIndex = Random.Range(0, tempList.Count);
                    Team team = tempList[randomIndex];
                    tempList.RemoveAt(randomIndex);
                    matchList.Add(new Match()
                    {
                        Teams = new[]
                        {
                            currTeamScript,
                            team
                        }
                    });
                }

                return matchList;
            }

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