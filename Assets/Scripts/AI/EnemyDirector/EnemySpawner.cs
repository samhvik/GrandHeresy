/*
    EnemySpawner.cs

    Handles all local spawning of mobs while players are detected.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

/*
    The EnemySpawner script and therefore object will behave accordingly:

    When spawned by the EnemyDirector script, the EnemySpawner object will be created in the center of
    the players' midpoint. The EnemySpawner object shall be a large cylinder of radius 'n.'
    
    'n' should be larger than the average camera zoom, but smaller than 1.5x the camera's furthest zoom.

    The EnemySpawner object will travel at a speed slower than the players' walking speed. (eg 70% walking speed)

    The EnemyDirector script will destroy the EnemySpawner object when it exists too far from the players. When
    destroyed, the global 'detected' state will revert back to false. When the EnemySpawer object is destroyed, all
    enemies spawned by it (if any still exist) shall be killed (destroyed).

    While it exists, the EnemySpawner object shall spawn enemies in groups of 1-6 every 5-20 seconds. It will spawn
    these mobs within the cylinder at a random yet valid location every iteration. 
    
    (A location is valid if the y value is equal to the general level of the playing field. Basically, we don't
    want to spawn mobs inside trees, hills, crevasses, etc.)
*/