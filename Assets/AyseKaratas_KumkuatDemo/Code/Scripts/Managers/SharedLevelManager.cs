using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PanteonDemo
{
    public class SharedLevelManager : PersistentSingleton<SharedLevelManager>
    {
        [SerializeField] private List<Team> _teamList = new List<Team>();

        public List<Team> TeamList => _teamList;
    }
}