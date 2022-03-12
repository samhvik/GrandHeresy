using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Chase")]
public class AIChaseAction : AIAction
{
    public override void Act(AIStateController controller){
        Chase(controller);
    }
    private void Chase(AIStateController controller){
        controller.navMeshAgent.destination = controller.chaseTarget.position;
        controller.navMeshAgent.isStopped = false;
        // speed up chasing units
        controller.navMeshAgent.speed     = controller.enemyStats.moveSpeed;
        controller.navMeshAgent.acceleration = 15;
        controller.navMeshAgent.angularSpeed = 300;
        controller.Combat(true);
        //Debug.Log("Play the Combat Music Sounds Here");

        /* Check if AI can see or unsee the Player
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov != null && fov.visibleTarget != null){
            Debug.Log("Player SPotted");
        } else {Debug.Log("no vision of player");} */
    }
    // WITH NEW CHASE ALGO. Where the player is in combat until X Distance, have that distance mod "breaking" check if combat is now off
    // dont have patrol update our combat status
}
