/*
    EnemySpawner.cs

    Handles all local spawning of mobs while players are detected.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject hordeEnemyToSpawn; // the horde enemy which will target a player automatically during spawn
    public GameObject RangedEnemyToSpawn; // the ranged enemy which will target a player automatically during spawn
    private float TimeElapsed;
    // set player midpoint
    // Temp Player Object until player grabbing is more dynamic
    public GameObject Player;
    // Temp Manager Gameobject until I move things over to Director for calls
    public AIManager Director;
    void Start(){  
        TimeElapsed = 0f;
        StartCoroutine(CheckMidpointDistance()); // delay checking where the midpoint is
    }

    // Tutorial Recommended delay on execution
    IEnumerator CheckMidpointDistance(){
        yield return new WaitForSeconds(0.5f);
        // if midpointDistance > tolerance:
        // GameValues.inCombatStatus = false;
    }
    //TO DO
    // Have EnemyDirector Call EnemySpawner Instead when an objective is active
    // figure out the tolerance for moving the enemyspawner
    // Implement the Exit Combat Status && Merge AIManager and EnemyDirector
    // Grab Objective Count for spawnRate
    // Grab Players Randomly as Targets for newly spawned AI
    
    void Update(){
        // update spawning range based on the midpoint. 
        // do this every frame for accuracy

        if(spawnRate(5)){ // replace 5 with objectives completed
            // spawn enemies randomly within the distance
            int waveNum = numberToSpawn();

            for(int i = 0; i < waveNum; i++){
                // pos += midpoint.position; // start the spawn point from the Player's Midpoint
                var pos = Random.insideUnitCircle * new Vector3(25, 1, 25); // Random Point within a Circle; *5 is the Radius of the circle
                GameObject nAI = Instantiate(hordeEnemyToSpawn, pos, Quaternion.LookRotation(-pos));
                // Setup Newly Spawned AI
                var newController = nAI.GetComponent<AIStateController>();
                newController.SetupAI(true, new List<Transform>()); // don't need to pass a waypoint list for these spawned ones
                newController.manager = Director;                   
                newController.chaseTarget = Player.transform;
                nAI.GetComponent<AIFOV>().visibleTarget = Player.transform; // This will be a random player
            }
            TimeElapsed = 0f; // reset timer upon exit of WaveSpawn
        }
    }

    // change the range of random to influence amount per group spawned
    private int numberToSpawn(){
        return Random.Range(1, 6);//6);
    }

    // just subtly increase the frequency to spawn enemies based on objective completion for now
    // grab the gamevalue for completedObjectives at some point
    private bool spawnRate(int completedObjectives){
        var complObj = completedObjectives;
        if(complObj <= 0){ complObj = 1; } // don't divide by zero
        TimeElapsed += Time.deltaTime;
        // default 20 second spawn timer reduced by number of completed objectives
        // once completedObjectives is a static VAR we wont need to pass in a rate
        return (TimeElapsed >= 20/complObj);
    }
}

/*
    The EnemySpawner script and therefore object will behave accordingly:

    When spawned by the EnemyDirector script, the EnemySpawner object will be created in the center of
    the players' midpoint. The EnemySpawner object shall be a large cylinder of radius 'n.'
    
    'n' should be larger than the average camera zoom, but smaller than 1.5x the camera's furthest zoom.

    The EnemySpawner object will travel at a speed slower than the players' walking speed. (eg 70% walking speed)
    It will always travel towards the players' midpoint.

    The EnemyDirector script will destroy the EnemySpawner object when it exists too far from the players. When
    destroyed, the global 'detected' state will revert back to false. When the EnemySpawer object is destroyed, all
    enemies spawned by it (if any still exist) shall be killed (destroyed).

    While it exists, the EnemySpawner object shall spawn enemies in groups of 1-6 every 5-20 seconds. It will spawn
    these mobs within the cylinder at a random yet valid location every iteration. 
    
    (A location is valid if the y value is equal to the general level of the playing field. Basically, we don't
    want to spawn mobs inside trees, hills, crevasses, etc.)
*/

// 