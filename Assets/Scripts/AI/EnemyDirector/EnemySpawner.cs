/*
    EnemySpawner.cs

    Handles all local spawning of mobs while players are detected.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject hordeEnemyToSpawn; // the horde enemy which will target a player automatically during spawn
    public GameObject RangedEnemyToSpawn; // the ranged enemy which will target a player automatically during spawn
    public GameObject CameraMidpoint;
    private int maxSpawns; // max number of enemies to spawn
    private int maxSpawnChecks;
    private Transform midpoint;

    void Start(){
        maxSpawns   = 5;
        maxSpawnChecks = 10;
    }
    
    void Update(){
        // update spawning range based on the midpoint. 
        // do this every frame for accuracy
        midpoint = CameraMidpoint.transform;
        if(spawnRate(GameValues.instance.objectivesCompleted)){
            // spawn enemies randomly within the distance
            int waveNum = numberToSpawn();
            for(int i = 1; i <= waveNum; i++){ // start at 1 for ranged enemy spawning math
                // get valid spawn points
                var pos = getRandPoint();
                var locale = CheckBounds(pos);
                if(locale.hit == false){ continue; } // if it FAILED to find a valid location just skip
                
                if ((waveNum/2) % i == 0){ // spawn a Ranged Enemy, pseudo randomized amount based on minSpawns in numToSpawn()
                    enemyCreator(RangedEnemyToSpawn, locale.position);
                }
                enemyCreator(hordeEnemyToSpawn, locale.position);
            }
            GameValues.instance.aiSpawnTimer = 0f; // reset timer upon exit of WaveSpawn
        }
    }

    // change the range of random to influence amount per group spawned
    private int numberToSpawn(){
        return Random.Range(2, maxSpawns);
    }

    private Vector3 getRandPoint(){
        // option: ensure no spawning too close to players?
        var pos = new Vector3(Random.insideUnitSphere.x * 15f, 0, Random.insideUnitSphere.z * 15f);
        return pos += midpoint.position; 
    }

    // Check the boundaries of the position to spawn enemies
    private NavMeshHit CheckBounds(Vector3 p){
        int i = 0;
        NavMeshHit loc;
        // check if valid location to spawn up to max checks
        do{
            NavMesh.SamplePosition(p, out loc, 6.0f, 1);
            i++;
        } while(i < maxSpawnChecks && !loc.hit);
        
        return loc;
    }

    // grab a player based on the number of players
    private Transform GrabAPlayer(){
        var pAmount = GameValues.instance.numPlayers;
        if (pAmount == 1){
            //Debug.Log("SinglePlayer");
            return GameValues.instance.players[0].transform;
        }
        // grab a player to focus
        return GameValues.instance.players[Random.Range(0, pAmount - 1)].transform;
    }

    // just subtly increase the frequency to spawn enemies based on objective completion for now
    private bool spawnRate(int completedObjectives){
        var complObj = completedObjectives; //add to this value to increase spawn rate 
        if(complObj <= 0){ complObj = 1; } // don't divide by zero, this is our default spawn rate
        GameValues.instance.aiSpawnTimer += Time.deltaTime;
        // default 20 second spawn timer reduced by number of completed objectives
        return (GameValues.instance.aiSpawnTimer >= 10/complObj);
    }

    // Create and Setup the new batch of enemies
    private void enemyCreator(GameObject prefab, Vector3 pos){
        // we use CheckBounds for making sure pos is valid
        GameObject nAI = Instantiate(prefab, pos, Quaternion.LookRotation(-pos));
        // Setup Newly Spawned AI
        var newController = nAI.GetComponent<AIStateController>();
        newController.SetupAI(true, new List<Transform>()); // don't need to pass a waypoint list for these spawned ones
        // Setup Chase Target
        Transform playerT = GrabAPlayer();               
        newController.chaseTarget = playerT;
        nAI.GetComponent<AIFOV>().visibleTarget = playerT; // This will be a random player
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