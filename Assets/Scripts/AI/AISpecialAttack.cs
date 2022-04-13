using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Special Attack")]
public class AISpecialAttack: AIAction
{
    public override void Act(AIStateController controller){
        Attack(controller);
    }

    private void Special(AIStateController c){

    }

    private void Attack(AIStateController controller){
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) return;

        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){ // visible target found
            // do special attack IF possible
            //if(controller.enemyStats.SpecialCD != 0 && controller.CheckIfCountdownElapse(controller.enemyStats.SpecialCD)){
            //    controller.navMeshAgent.isStopped = true;
                //Debug.Log("Attack Animation Here");
                // Projector / Draw Circle on the ground
                // Debug.Log("Attack Sound Here");
                // Cast time later just get it to work

                // update the players health
            //    fov.visibleTarget.gameObject.GetComponent<PlayerInventory>().UpdateHealth(controller.enemyStats.damage);
            //}

            // basic attack
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD) && controller.checkRange()){
                //Stop Controller from Moving briefly for animations
                controller.navMeshAgent.isStopped = true;
                //Debug.Log("Attack Animation Here");
                //Debug.Log("Attack Sound Here");
                Debug.Log("Play Attack Sound Here");
                // update the players health
                fov.visibleTarget.gameObject.GetComponent<PlayerInventory>().UpdateHealth(controller.enemyStats.damage);
            }
            controller.navMeshAgent.isStopped = false;
        }
    }
}

// Ranged Attack Issues
// using a bullet since the script is already made, altering the "round" script to detect when a bullet hits a "Player"
// would induce friendly fire into the game. a way around that?
// IDEAS
// copy paste round to be something else and instantiate as that

// ???