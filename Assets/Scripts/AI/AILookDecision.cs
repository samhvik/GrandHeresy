using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Look")]
public class AILookDecision : AIDecision {

    public override bool Decide(AIStateController controller)
    {
        return Look(controller);
    }

    private bool Look(AIStateController controller)
    {
        // controller.eyes is a variable in AIStateController.cs
        // wherever the AI is looking is its "eyes"
        // use spherecast as it has a smaller margin of error when seeking targets out
        RaycastHit hit;
        Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

        if (Physics.SphereCast (controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.lookRange)
            && hit.collider.CompareTag ("Player")) {
            controller.chaseTarget = hit.transform;
            return true;
        } else {
            return false;
        }
    }
    /*
    private bool Look(AIStateController controller)
    {
        AIFOV fov = controller.GetComponent<FieldOfView>();
        if(fov == null) Debug.Log("no FOV Script I can't see"); return false;
        
        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player)){
            controller.target = fov.visibleTarget;
            return true;
        }
        return false;
    }*/
}