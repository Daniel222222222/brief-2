using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Attack
/// </summary>
public class FightManager : MonoBehaviour
{
    public BattleSystem battleSystem; //A reference to our battleSystem script in our scene
    public Color drawCol = Color.gray; // A colour you might want to set the battle log message to if it's a draw.
    private float fightAnimTime = 2; //An amount to wait between initiating the fight, and the fight begining, so we can see some of that sick dancing.
    public Character character;

    //TODO this function is all you need to modify, in this script.
    //You just need determine who wins/loses/draws etc.
    IEnumerator Attack(Character teamACharacter, Character teamBCharacter)
    {
        float outcome = 0;// the outcome from the fight, i.e. the % that the winner has won by...fractions could help us calculate this, but start with whole numbers i.e. 0 = draw, and 1 = 100% win.
        Character winner = teamACharacter;//defaulting the winner to TeamA.
        Character defeated = teamBCharacter;//defaulting the loser to TeamB.

        // Tells each dancer that they are selcted and sets the animation to dance.
        SetUpAttack(teamACharacter);
        SetUpAttack(teamBCharacter);

        // Tells the system to wait X number of seconds until the fight to begins.
        yield return new WaitForSeconds(fightAnimTime);

        // We want to get some battle points from each of our characters...instead of just 0....is there a function in the Character script that could help us?
        int teamABattlePoints = teamACharacter.ReturnBattlePoints();
        int teamBBattlePoints = teamBCharacter.ReturnBattlePoints();

        
        if (teamABattlePoints <= 0 || teamBBattlePoints <= 0)
        {
            Debug.LogWarning("Player or NPC battle points is 0, most likely the logic has not be setup for this yet");
        }
        Debug.Log(teamABattlePoints);
        Debug.Log(teamBBattlePoints);
        if (teamABattlePoints > teamBBattlePoints)
        {
            outcome = 1 - ((float)teamBBattlePoints / (float)teamABattlePoints);
            Debug.Log("raw wins");
            winner = teamACharacter;
            defeated = teamBCharacter;
            BattleLog.Log("Raw wins!", winner.myTeam.teamColor);

        }
        else if (teamABattlePoints < teamBBattlePoints)
        {
            outcome = 1 - ((float)teamBBattlePoints / (float)teamABattlePoints);
            Debug.Log("smackdown wins");
            defeated = teamACharacter;
            BattleLog.Log("Smackdown wins!", winner.myTeam.teamColor);

        }
        else
            {
            BattleLog.Log("Fight is a draw!", drawCol);
        }
        

        Debug.LogWarning("Attack called, may want to use the BattleLog.Log() to report the dancers and the outcome of their dance off.");

        // Pass on the winner/loser and the outcome to our fight completed function.
        FightCompleted(winner, defeated, outcome);
        yield return null;
    }

    #region Pre-Existing - No Modes Required
    /// <summary>
    /// Is called when two dancers have been selected and begins a fight!
    /// </summary>
    /// <param name="data"></param>
    public void Fight(Character TeamA, Character TeamB)
    {
        StartCoroutine(Attack(TeamA, TeamB));
    }

    /// <summary>
    /// Passes in a winning character, and a defeated character, as well as the outcome between -1 and 1
    /// </summary>
    /// <param name="winner"></param>
    /// <param name="defeated"></param>
    /// <param name="outcome"></param>
    private void FightCompleted(Character winner, Character defeated, float outcome)
    {
        var results = new FightResultData(winner, defeated, outcome);

        winner.isSelected = false;
        defeated.isSelected = false;

        battleSystem.FightOver(winner,defeated,outcome);
        winner.animController.BattleResult(winner,defeated,outcome);
        defeated.animController.BattleResult(winner, defeated, outcome);
    }

    /// <summary>
    /// Sets up a dancer to be selected and the animation to start dancing.
    /// </summary>
    /// <param name="dancer"></param>
    private void SetUpAttack(Character dancer)
    {
        dancer.isSelected = true;
        dancer.GetComponent<AnimationController>().Dance();
        BattleLog.Log(dancer.charName.GetFullCharacterName() + " Selected", dancer.myTeam.teamColor);
    }
    #endregion  
}
