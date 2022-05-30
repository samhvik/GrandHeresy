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
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) {Debug.Log("no FOV Script I can't see"); return false;}
        
        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){
            //Debug.Log("");
            controller.chaseTarget = fov.visibleTarget;
            return true;
        }
        return false;
    }
}