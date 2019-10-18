using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Functions to complete:
/// - Init Teams
/// </summary>
public class DanceTeamInit : MonoBehaviour
{
    public DanceTeam teamA, teamB; // A reference to our teamA and teamB DanceTeam instances.

    public GameObject dancerPrefab; // This is the dancer that gets spawned in for each team.
    public int dancersPerSide = 3; // This is the number of dancers for each team, if you want more, you need to modify this in the inspector.
    public CharacterNameGenerator nameGenerator; // This is a reference to our CharacterNameGenerator instance.
    private CharacterName[] teamACharacterNames; // An array to hold all our character names of TeamA.
    private CharacterName[] teamBcharacterNames; // An array to hold all the character names of TeamB

    /// <summary>
    /// Called to iniatlise the dance teams with some dancers :D
    /// </summary>
    public void InitTeams()
    {
        Debug.LogWarning("InitTeams called, needs to generate names for the teams and set them with teamA.SetTroupeName and teamA.InitialiseTeamFromNames");
        // We need to set out team names using teamA.SetTroupeName.
        // We need to generate some character names for our teams to use from our CharacterNameGenerator.
        // We need to spawn in some dancers using teamA.InitialiseTeamFromNames.


        Debug.LogWarning("InitTeams called, needs to create character names via CharacterNameGenerator and get them into the team.InitaliseTeamFromNames");
    }
}
