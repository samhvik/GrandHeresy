using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Scan")]
public class AIScanDecision : AIDecision
{
    public override bool Decide(AIStateController controller){
        return Scan(controller);
    }

    private bool Scan(AIStateController controller){
        controller.navMeshAgent.isStopped = true;
        //Debug.Log("I should be scanning...");
        // turn speed can be changed based on EnemyStats
        controller.transform.Rotate(0, controller.enemyStats.searchTurnSpeed * Time.deltaTime, 0);
        // check if the countdown has elapse for how long the enemy should "scan" for
        // this lets it be used with a patrolling mob etc.
        return controller.CheckIfCountdownElapse(controller.enemyStats.searchDuration);
    }
}
