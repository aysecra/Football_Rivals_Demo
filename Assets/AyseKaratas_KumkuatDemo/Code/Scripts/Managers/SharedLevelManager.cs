using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    public class SharedLevelManager : PersistentSingleton<SharedLevelManager>
    {
        [SerializeField] private List<Team> _teamList = new List<Team>();
        [SerializeField] private List<TeamMember> _teamMemberList = new List<TeamMember>();

        public List<Team> TeamList => _teamList;
    }
}