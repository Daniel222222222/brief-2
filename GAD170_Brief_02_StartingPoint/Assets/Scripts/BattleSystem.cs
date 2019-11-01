using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Do Round
/// - Fight Over
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public DanceTeam teamA,teamB; //References to TeamA and TeamB
    public FightManager fightManager; // References to our FightManager.
    public Character character;
    public float battlePrepTime = 2;  // the amount of time we need to wait before a battle starts
    public float fightCompletedWaitTime = 2; // the amount of time we need to wait till a fight/round is completed.
    
    /// <summary>
    /// This occurs every round or every X number of seconds, is the core battle logic/game loop.
    /// </summary>
    /// <returns></returns>
    IEnumerator DoRound()
    {
       
        // waits for a float number of seconds....
        yield return new WaitForSeconds(battlePrepTime);

        //checking for no dancers on either team
        if(teamA.allDancers.Count == 0 && teamB.allDancers.Count == 0)
        {
            Debug.LogWarning("DoRound called, but there are no dancers on either team. DanceTeamInit needs to be completed");
            // This will be called if there are 0 dancers on both teams.

        }
        else if (teamA.activeDancers.Count > 0 && teamB.activeDancers.Count > 0)
        {
            Debug.LogWarning("DoRound called, it needs to select a dancer from each team to dance off and put in the FightEventData below");
            //You need to select two random or engineered random characters to fight...so one from team a and one from team b....
            //We then need to pass in the two selected dancers into fightManager.Fight(CharacterA,CharacterB);
            //fightManager.Fight(charA, charB);
            Character characterA = teamA.activeDancers[Random.Range(0, teamA.activeDancers.Count)];
            Character characterB = teamB.activeDancers[Random.Range(0, teamB.activeDancers.Count)];
            fightManager.Fight(characterA, characterB);

        }
        else
        {
            // IF we get to here...then we have a team has won...winner winner chicken dinner

            // We need to determine a winner...but how?...maybe look at the previous if statements for clues?
            if(teamA.activeDancers.Count > 0 && teamB.activeDancers.Count == 0)
            {
                teamA.EnableWinEffects();
                BattleLog.Log(teamA.danceTeamName, teamA.teamColor);
            }

            else if (teamB.activeDancers.Count > 0 && teamA.activeDancers.Count == 0)
            {
                teamB.EnableWinEffects();
                BattleLog.Log(teamB.danceTeamName, teamB.teamColor);
            }

            
            

            Debug.Log("DoRound called, but we have a winner so Game Over");
          
        }
    }

    // This is where we can handle what happens when we win or lose.
    public void FightOver(Character winner, Character defeated, float outcome)
    {
        Debug.LogWarning("FightOver called, may need to check for winners and/or notify teams of zero mojo dancers");   
        // assign damage...or if you want to restore health if they want that's up to you....might involve the character script.
        if(outcome == 0)
        {
            winner.CalculateXP(outcome);
            defeated.CalculateXP(outcome);
        }
        
        else
        {
            defeated.DealDamage(outcome);
            winner.CalculateXP(outcome);
        }
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(HandleFightOver());
    }



    /// <summary>
    /// Used to Request A round.
    /// </summary>
    public void RequestRound()
    {
        //calling the coroutine so we can put waits in for anims to play
        StartCoroutine(DoRound());
    }

    /// <summary>
    /// Handles the end of a fight and waits to start the next round.
    /// </summary>
    /// <returns></returns>
    IEnumerator HandleFightOver()
    {
        yield return new WaitForSeconds(fightCompletedWaitTime);
        teamA.DisableWinEffects();
        teamB.DisableWinEffects();
        Debug.LogWarning("HandleFightOver called, may need to prepare or clean dancers or teams and checks before doing GameEvents.RequestFighters()");
        RequestRound();
    }
}
