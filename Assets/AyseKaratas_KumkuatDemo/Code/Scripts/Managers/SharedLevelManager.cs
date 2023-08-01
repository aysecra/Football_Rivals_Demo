using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    public class SharedLevelManager : PersistentSingleton<SharedLevelManager>
    {
        [SerializeField] private List<Team> _teamList = new List<Team>();
        [SerializeField] private List<TeamMember> _teamMemberList = new List<TeamMember>();

        public List<Team> TeamList => _teamList;

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
    }
}