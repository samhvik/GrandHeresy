using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Look")]
public class LookDecision : AIDecision {

    public override bool Decide(AIStateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(AIStateController controller)
    {
        // controller.eyes is a variable in AIStateController.cs
        // wherever the AI is looking is its "eyes"
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
}