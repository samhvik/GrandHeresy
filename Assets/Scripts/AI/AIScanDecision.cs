using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Scan")]
public class AIScanDecision : AIDecision
{
    public override bool Decide(AIStateController controller){
        bool noEnemy = Scan(controller);
        return noEnemy;
    }

    private bool Scan(AIStateController controller){
        controller.navMeshAgent.isStopped = true;
        // turn speed can be changed based on EnemyStats
        controller.transform.Rotate(0, controller.enemyStats.searchTurnSpeed * Time.deltaTime, 0);
        // check if the countdown has elapse for how long the enemy should "scan" for
        return controller.CheckIfCountdownElapse(controller.enemyStats.searchDuration);
    }
}
