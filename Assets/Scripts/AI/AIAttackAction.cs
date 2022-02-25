using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Attack")]
public class AIAttackAction : AIAction
{
    public override void Act(AIStateController controller){
        Attack(controller);
    }

    private void Attack(AIStateController controller){
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) return;
        
        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD) && controller.checkRange()){
                //GameValues.instance.UpdateHealth(controller.enemyStats.damage);
                //Debug.Log("Play Attack Sound Here");
                //Debug.Log("Attack");
                controller.stateTimeElapsed = 0;
            }
        }
    }
}
