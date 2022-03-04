/*
    EnemyDirector.cs

    Handles all AI spawning.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    public List<Transform> waypoints;
    private AIStateController[] enemies; // should change down the line to use less resources finding stuff
    
    private void Start(){
        GameValues.inCombatStatus = false;
        // Initialize the Pre-Set Enemies
        enemies = FindObjectsOfType<AIStateController>();
        foreach(var e in enemies){
            e.SetupAI(true, waypoints);
        }
        StartCoroutine(CheckForCombat()); // forcing a delay on combatFlag Checking
    }

    // check for combat once in "awhile" not every frame
    // it'll be TRUE a lot but untrue less often
    IEnumerator CheckForCombat(){
        yield return new WaitForSeconds(0.3f);
        if(GameValues.inCombatStatus){
            // check if EnemySpawner Exists
            // if Not Spawn it then execute the rest
            // spawn EnemySpawner
            // Enemyspawner will handle enemy spawning
        } else{
            Debug.Log("Not In Combat");
            // Not in Combat
            // check if EnemySpawner is enabled
            // if Enabled Turn OFF
            // if you wanted to check any AI left behind we could do so here
        }
    }

    // TODO
    // Finish EnemySpawner
    // Enable / Disable EnemySpawner based on Combat Status
    // Turn Combat OFF when Player Midpoint have reached X distance away from the start of combat
              // -- This may need some tuning in terms of how we want combat to behave
    // Call spawnRate; while(Combat) instead of Update(); on EnemySpawner rather than have it be an update
    // Flag Combat status when Objective has started
}

/*
    Breakdown on how AI spawning should work:

    An 'EnemySpawner' object will spawn at the midpoint location of the players
    if and only if:
        a. A player has just been detected by a patrolling enemy.
        b. A player has just initiated an objective sequence.
    
    The EnemyDirector script will spawn an EnemySpawner object when either of these conditions are met.
    The EnemySpawner object will handle all local spawning of mobs and destroys itself once the players are no
    longer detected.

    The EnemyDirector script will spawn patrolling enemies broadly around the player positions, and keep track of 
    those enemies. When those enemies are too far from the players, the EnemyDirector will destroy them.

    This script should utilize methods for spawning EnemySpawner scripts, so that we can incorporate 'detected' sounds
    and such. Please make sure all code is clean, labeled, and modular.
*/