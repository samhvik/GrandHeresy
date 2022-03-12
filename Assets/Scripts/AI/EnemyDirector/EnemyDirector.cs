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
    public GameObject CameraMidpoint;
    private GameObject player;
    
    private void Start(){
        // make sure the spawner is off at start of gameplay
        if(this.GetComponent<EnemySpawner>().enabled){
            this.GetComponent<EnemySpawner>().enabled = false;
        }
        GameValues.inCombatStatus = false;
        // Initialize the Patrol Enemies
        enemies = FindObjectsOfType<AIStateController>();
        foreach(var e in enemies){
            e.SetupAI(true, waypoints);
        }
        this.transform.position = CameraMidpoint.transform.position;
        StartCoroutine(CheckForCombat()); // forcing a delay on combatFlag Checking
    }

    // check for combat once in "awhile" not every frame
    // it'll be TRUE a lot but untrue less often
    IEnumerator CheckForCombat(){
        while(true){
            yield return new WaitForSeconds(0.3f);
            //Debug.Log("Cam: " + CameraMidpoint.transform.position);
            //Debug.Log("This Pos:" + this.transform.position);
            float dist = Vector3.Distance(CameraMidpoint.transform.position,
                    this.transform.position);
            //Debug.Log(dist);
            if(GameValues.inCombatStatus){
                // check if EnemySpawner Exists
                // Enemyspawner will handle enemy spawning
                if(!this.GetComponent<EnemySpawner>().enabled){
                    this.GetComponent<EnemySpawner>().enabled = true;
                    moveDirector();
                    dist = 0f;
                    //Debug.Log("Combat Enabled" + dist);
                }
                if(dist >= 12){
                    //Debug.Log("Combat Should End Now" + dist);
                    // Despawn Enemies here if wanted
                    GameValues.inCombatStatus = false;
                    this.GetComponent<EnemySpawner>().enabled = false;
                }
            }
            this.transform.position = CameraMidpoint.transform.position * 0.80f;
        }
    }

    public void moveDirector(){
        this.transform.position = CameraMidpoint.transform.position;
    }

    // TODO
    // Turn Combat OFF when Player Midpoint have reached X distance away from the start of combat
              // -- This may need some tuning in terms of how we want combat to behave
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