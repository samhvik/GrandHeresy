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
    // Temp Manager Gameobject until I move things over to Director for calls
    public GameObject CameraMidpoint;
    private Transform midpoint;
    void Start(){  
        TimeElapsed = 0f;
    }
    
    void Update(){
        // update spawning range based on the midpoint. 
        // do this every frame for accuracy
        midpoint = CameraMidpoint.transform;
        if(spawnRate(GameValues.instance.objectivesCompleted)){
            // spawn enemies randomly within the distance
            int waveNum = numberToSpawn();
            for(int i = 0; i < waveNum; i++){
                // Improvement: Add a value to X and Y of pos
                // to prevent AI spawning inside / really close to the player
                //
                // Random Point within a Circle; *25f is the Radius of the circle, 0 is our floor level
                var pos = new Vector3(Random.insideUnitSphere.x * 25f, 0, Random.insideUnitSphere.z * 25f);
                pos += midpoint.position; // move the spawnpoint near the player
                // we use CheckBounds for making sure pos is valid
                // Physics.CheckSphere(transform.position, sphereRadius, LayerMask) // Might be useful for boundary spawning
                GameObject nAI = Instantiate(hordeEnemyToSpawn, CheckBounds(pos), Quaternion.LookRotation(-pos));
                // Setup Newly Spawned AI
                var newController = nAI.GetComponent<AIStateController>();
                newController.SetupAI(true, new List<Transform>()); // don't need to pass a waypoint list for these spawned ones
                // Setup Chase Target
                Transform playerT = GrabAPlayer();               
                newController.chaseTarget = playerT;
                nAI.GetComponent<AIFOV>().visibleTarget = playerT; // This will be a random player
            }
            TimeElapsed = 0f; // reset timer upon exit of WaveSpawn
        }
    }

    // change the range of random to influence amount per group spawned
    private int numberToSpawn(){
        return Random.Range(1, 6);//6);
    }

    // current Greybox bounds are roughly 
    // -85 <= x <= 100
    // -65 <= z <= 115
    private Vector3 CheckBounds(Vector3 p){
        if(p.x < -85){ p.x = -85; }
        if(p.x > 100){ p.x = 100; }
        if(p.z < -65){ p.z = -65; }
        if(p.z > 115){ p.z = 115; }
        p.y = -20;
        return p;
    }

    // grab a player based on the number of players
    private Transform GrabAPlayer(){
        var pAmount = GameValues.instance.numPlayers;
        if (pAmount == 1){
            //Debug.Log("SinglePlayer");
            return GameValues.instance.players[0].transform;
        }
        // grab first player
        return GameValues.instance.players[Random.Range(0, pAmount - 1)].transform;
    }

    // just subtly increase the frequency to spawn enemies based on objective completion for now
    // grab the gamevalue for completedObjectives at some point
    private bool spawnRate(int completedObjectives){
        var complObj = completedObjectives; //add to this value to increase spawn rate 
        if(complObj <= 0){ complObj = 1; } // don't divide by zero, our default spawn rate
        TimeElapsed += Time.deltaTime;
        // default 20 second spawn timer reduced by number of completed objectives
        // once completedObjectives is a static VAR we wont need to pass in a rate
        return (TimeElapsed >= 10/complObj);
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