using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerData
{
    public int LevelIndex;
    public string LevelName;
}

[Serializable]
public class Team
{
    public string Title;
    public Sprite Image;
    public uint Star;
}

[Serializable]
public class TeamMember
{
    public string Name;
    public uint Level;
}

