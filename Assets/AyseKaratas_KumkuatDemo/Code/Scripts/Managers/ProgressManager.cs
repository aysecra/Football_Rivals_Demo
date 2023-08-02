using System;
using System.Collections.Generic;
using UnityEngine;

namespace KumkuatDemo
{
    [System.Serializable]
    public class LevelProgress
    {
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")]
        public string LevelName;
    }

    public class ProgressManager : PersistentSingleton<ProgressManager>
        , EventListener<LevelEvent>
    {
        [SerializeField] private List<LevelProgress> _levels;
        [SerializeField] private string _playerName = "Developer Player";

        private PlayerData _playerData;
        private const string _playerDataPrefKey = "PlayerData";
        private TeamMember _playerTeamMember;

        protected override void Awake()
        {
            base.Awake();

            if (!PlayerPrefs.HasKey(_playerDataPrefKey))
            {
                _playerData = new PlayerData
                {
                    LevelName = _levels[0].LevelName,
                    LevelIndex = 0,
                    CurrentTeamName = "",
                    PlayerLevel = 1,
                    Name = _playerName
                };
            }

            else
            {
                _playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(_playerDataPrefKey));

                if (_playerData.CurrentTeamName != "")
                {
                    Team team = SharedLevelManager.Instance.GetTeam(_playerData.CurrentTeamName);
                    _playerTeamMember = new TeamMember()
                    {
                        Name = _playerName,
                        Level = (uint) _playerData.PlayerLevel,
                        Team = team.Title
                    };
                    SharedLevelManager.Instance.AddPlayerToTeam(team, _playerTeamMember);
                }
            }
        }

        private void SetPlayerData()
        {
            PlayerPrefs.SetString(_playerDataPrefKey, JsonUtility.ToJson(_playerData));
        }

        private void SetNextLevel()
        {
            int index = _playerData.LevelIndex + 1 < _levels.Count ? _playerData.LevelIndex + 1 : 0;
            _playerData.LevelName = _levels[index].LevelName;
            _playerData.LevelIndex = index;
            SetPlayerData();
        }

        public string GetCurrentLevelName()
        {
            return _playerData.LevelName;
        }

        public string GetNextLevelName()
        {
            int index = _playerData.LevelIndex + 1 < _levels.Count ? _playerData.LevelIndex + 1 : 0;
            return _levels[index].LevelName;
        }

        public string GetCurrentTeam()
        {
            return _playerData.CurrentTeamName;
        }

        public void SetCurrentTeam(Team team)
        {
            _playerData.CurrentTeamName = team.Title;
            
            _playerTeamMember = new TeamMember()
            {
                Name = _playerName,
                Level = (uint) _playerData.PlayerLevel,
                Team = team.Title
            };
            
            SharedLevelManager.Instance.AddPlayerToTeam(team, _playerTeamMember);

            SetPlayerData();
        }

        private void OnEnable()
        {
            EventManager.EventStartListening<LevelEvent>(this);
        }

        private void OnDisable()
        {
            EventManager.EventStopListening<LevelEvent>(this);
        }

        public void OnEventTrigger(LevelEvent currentEvent)
        {
            if (currentEvent.State == LevelState.Completed)
            {
                SetNextLevel();
            }
        }
    }
}