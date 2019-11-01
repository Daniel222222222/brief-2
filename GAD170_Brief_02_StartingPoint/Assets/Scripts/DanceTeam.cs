using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Functions to complete:
/// - Remove From Active
/// - Add Dancer
/// </summary>
public class DanceTeam : MonoBehaviour
{
    public enum Direction : int { Left = -1, Right = 1 }; // An Enum used to hold Left and Right directions/determines which side of the screen dancers spawn in at.
    const float DancerSpaceing = 2; // The distance between each dancer when they spawn in.
    public Color teamColor = Color.white; // Our team colours, we can change these in the inspectors.
    [SerializeField]
    public string danceTeamName; // this is set from our SetTroupeName Function.
    public Transform lineUpStart; // this is just a transform in our scene where we set the spawn position of our team.
    public GameObject fightWinContainer; // This is just a point light in our scene that gets turned on and off if we win just to make it look fancy.
    public Text troupeNameText; // this is just a text element in the scene that set by the troupe name function.
    public List<Character> allDancers; // A list of all the dancers on our team.
    public List<Character> activeDancers; // A list of our currently active dancers, when they die they need to be removed from this list. 

    /// <summary>
    /// Adds a new dancer to our dance team
    /// </summary>
    /// <param name="dancer"></param>
    public void AddNewDancer(Character dancer)
    {
               // we probably want to add our new dancers to our all dancers and our active dancers lists here..

        allDancers.Add(dancer);
        activeDancers.Add(dancer);
    }

    /// <summary>
    /// Removes a dancer to our dance team.
    /// </summary>
    /// <param name="dancer"></param>
    public void RemoveFromActive(Character dancer)
    {
        
        // This gets called when our team mate dies :(
        // We probably want to remove the dancer passed in from our active dancer list.
        activeDancers.Remove(dancer);

    }

    /// <summary>
    /// Takes in a dancer prefab to spawn, a direction 1 = right, -1 = left, and an Array of Names
    /// </summary>
    /// <param name="dancerPrefab"></param>
    /// <param name="direction"></param>
    /// <param name="names"></param>
    public void InitaliseTeamFromNames(GameObject dancerPrefab, Direction screenDirection, CharacterName[] names)
    {
        for (int i = 0; i < names.Length; i++)
        {
            //make one
            var newDancer = Instantiate(dancerPrefab, lineUpStart.position + lineUpStart.right * i * DancerSpaceing * ((int)screenDirection), dancerPrefab.transform.rotation);
            //fix its rotation, animations are often a pain
            newDancer.transform.forward = -lineUpStart.right;

            //give it a name and add it to the team
            var aChar = newDancer.GetComponent<Character>();
            aChar.AssignName(names[i]);
            aChar.myTeam = this;
            AddNewDancer(aChar);
        }
    }

    /// <summary>
    /// Sets the team's troupe name to something we pass in.
    /// </summary>
    /// <param name="name"></param>
    public void SetTroupeName(string name)
    {
        danceTeamName = name;
        if (troupeNameText != null)
        {
            troupeNameText.text = name;
        }
    }

   /// <summary>
   /// Enables the win effects for the winning dancer/team
   /// </summary>
    public void EnableWinEffects()
    {
        if (fightWinContainer != null)
        {
            fightWinContainer.SetActive(true);
            var l = fightWinContainer.GetComponentInChildren<Light>();
            if (l != null)
            {
                l.color = teamColor;
            }
        }
    }

    /// <summary>
    /// Disables the win effects for the winner/dancing team.
    /// </summary>
    public void DisableWinEffects()
    {
        if (fightWinContainer != null)
        {
            fightWinContainer.SetActive(false);
        }
    }
}
