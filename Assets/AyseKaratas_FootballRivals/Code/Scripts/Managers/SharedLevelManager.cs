using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FootballRivalsDemo
{
    /// <summary>
    /// Event is triggered when team member must update
    /// </summary>
    public struct UpdateTeamMemberEvent
    {
    }
    
    
    /// <summary>
    /// Event is triggered when attack list must update
    /// </summary>
    public struct UpdateAttackListEvent
    {
    }

    public class SharedLevelManager : PersistentSingleton<SharedLevelManager>
    {
        [SerializeField] private List<Team> _teamList = new List<Team>();
        [SerializeField] private List<TeamMember> _teamMemberList = new List<TeamMember>();

        public List<Team> TeamList => _teamList;

        private TeamMember _player;

        private void Start()
        {
            GetRandomAttackList(new Match()
            {
                Teams = new[] {_teamList[0], _teamList[1]}
            });
        }

        // the list of matches of the user's team with random teams is returned
        public List<Match> GetRandomMatchList(uint length)
        {
            length = _teamList.Count > length ? length : (uint) (_teamList.Count - 2);
            string currTeam = ProgressManager.Instance.GetCurrentTeam();
            if (currTeam != "")
            {
                List<Match> matchList = new List<Match>();
                List<Team> tempList = new List<Team>(_teamList);;
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

        // the list of specific match of user's players with random players is returned
        public Attack GetRandomAttackList(Match match)
        {
            string currTeam = ProgressManager.Instance.GetCurrentTeam();
            int length = 0;
            if (currTeam != "")
            {
                List<TeamMember> team1 = new List<TeamMember>();
                List<TeamMember> team2 = new List<TeamMember>();

                foreach (var member in _teamMemberList)
                {
                    if (match.Teams[0].Title == member.Team)
                    {
                        team1.Add(member);
                    }
                    
                    if (match.Teams[1].Title == member.Team)
                    {
                        team2.Add(member);
                    }
                }

                team1.Reverse();
                if (team1.Count > team2.Count)
                {
                    team1.RemoveRange(team2.Count, team1.Count-team2.Count);
                }
                
                else if (team2.Count > team1.Count)
                {
                    team2.RemoveRange(team1.Count, team2.Count-team1.Count);
                }
                
                team1  = team1.OrderBy(i => Guid.NewGuid()).ToList();
                team2  = team2.OrderBy(i => Guid.NewGuid()).ToList();

                return new Attack()
                {
                    Match = match,
                    Team1 = team1,
                    Team2 = team2
                };
            }

            return null;
        }

        // specific team of members is returned
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

        // team script is returned according to team name
        public Team GetTeam(string teamName)
        {
            foreach (var team in _teamList)
            {
                if (teamName == team.Title)
                    return team;
            }

            return null;
        }

        // this method is used when selecting team
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

        // this method is used when user changing team
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