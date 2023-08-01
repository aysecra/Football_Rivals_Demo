using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerData
{
    public int LevelIndex;
    public string LevelName;
    public Team CurrentTeam;
    public int PlayerLevel;
}

[Serializable]
public class Team
{
    public string Title;
    public Sprite Image;
    public uint MaxTeamMember = 20;
    public uint TeamMember = 0;
    public uint Star;
}

[Serializable]
public class TeamMember
{
    public string Name;
    public uint Level;
    public Team Team;
}

[Serializable]
public class Match
{
    public Team[] Teams = new Team[2];
}

[Serializable]
public class Attack
{
    public Match Match;
    public Dictionary<TeamMember, TeamMember> AttackMambers = new Dictionary<TeamMember, TeamMember>();
}