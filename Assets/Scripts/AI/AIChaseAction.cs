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
        controller.navMeshAgent.Resume();
    }
}
