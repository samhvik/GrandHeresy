using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Patrol")]
public class AIPatrolAction : AIAction
{
    public override void Act(AIStateController controller)
    {
        Patrol (controller);
    }

    // travel between set waypoints that are fed from the AI Manager
    private void Patrol(AIStateController controller)
    {
        controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller.navMeshAgent.isStopped = false;
        controller.navMeshAgent.speed     = controller.enemyStats.moveSpeed/2;
        //Debug.Log("I should be patrolling...");
        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) 
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }
}
