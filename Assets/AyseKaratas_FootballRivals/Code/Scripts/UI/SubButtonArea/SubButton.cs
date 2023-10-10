using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FootballRivalsDemo
{
    [Serializable]
    public enum SubButtonType
    {
        Profile,
        Team,
        Friends,
        News,
        Chat
    }
    
    public class SubButton : MonoBehaviour
    {
        [SerializeField] private SubButtonType _subButtonType;
        public void OnClickButton()
        {
            switch(_subButtonType)
            {
                case SubButtonType.Profile:
                    break;
                case SubButtonType.Team:
                    EventManager.TriggerEvent(new SelectTeamEvent(ProgressManager.Instance.GetCurrentTeam()));
                    break;
                case SubButtonType.Friends:
                    break;
                case SubButtonType.News:
                    break;
                case SubButtonType.Chat:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
