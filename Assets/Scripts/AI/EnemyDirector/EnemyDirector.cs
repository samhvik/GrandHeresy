/*
    EnemyDirector.cs

    Handles all AI spawning.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
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