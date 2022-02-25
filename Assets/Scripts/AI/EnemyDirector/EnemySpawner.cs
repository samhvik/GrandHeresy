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
    
    void Start(){  
        TimeElapsed = 0f;
     }

    
    void Update(){
        // update spawning range based on the midpoint. 
        // do this every frame for accuracy

        if(spawnRate(5)){
            // spawn enemies randomly within the distance
            //Debug.Log("Enemy Wave Spawn");
            //int waveNum = numberToSpawn();
            //var pos = Random Point In Circle;
            //for(int i = 0; i < waveNum; i++){
            //  Instantitate(hordeEnemyToSpawn, pos, Quaternion.identity);
            //  pos.z += 5 // add some offset so enemies dont spawn ontop of each other    
            //}

            /* maybe we choose a random player and set them as the target and have the default rotation
            // of the AI to be looking at that player.
               this could add some spice 
            */
            // TimeElapsed = 0f; // reset timer upon exit of WaveSpawn
        }
    }

    // change the range of random to influence group spawning
    private int numberToSpawn(){
        return Random.Range(1, 6);
    }

    // just subtly increase the frequency to spawn enemies based on objective completion for now
    // grab the gamevalue for completedObjectives at some point
    private bool spawnRate(int completedObjectives){
        var complObj = completedObjectives;
        if(complObj <= 0){ complObj = 1; }
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