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
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov != null && fov.visibleTarget != null){
            Debug.Log("Player SPotted");
        } else {Debug.Log("no vision of player");}
    }
}
