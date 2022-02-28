/*
    Extract.cs

    Handles the extraction procedure upon player interaction.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Extract : MonoBehaviour
{
    public int requiredAmount;
    public int currentAmount;
    public float extractionTime;
    public bool extractionOpen = false;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void CompletedMission()
    {
        currentAmount++;
    }

    public void Extraction()
    {
        if (IsReached())
        {
            extractionOpen = true;
            extractionTime = Random.Range(30.0f, 90.0f) * Time.deltaTime;
        }
    }
}

/*
    How extraction should work:

    Before players load into the map, a beacon location will be randomly selected and a beacon
    prefab will spawn in its place. (Similar to objective spawns)

    Players will be able to extract when all objectives are either Completed or Failed.

    When this condition is met, players will now be able to interact with the extraction beacon.

    When the extraction beacon is triggered, this script is essentailly activated and all players are
    immediately detected.

    When triggered, a timer will initiate. The time shall be random, between 30 and 90 seconds.
    Players are expected to survive for the duration of the beacon's timer.

    The beacon will also have a set radius that, when a player leaves, will cause the current beacon
    procedure to completely stop. Players will have to re-interact with the beacon to restart the process.

    Upon the timer's completion, all players are "warped" back to HQ and the game is over.
    The players will then be taken to a recap screen that lets them know the run was completed,
    display in-game stats, etc. They will then have the option to return to the main menu/map selection.
*/
