using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Attack")]
public class AIAttackAction : AIAction
{
    public override void Act(AIStateController controller){
        Attack(controller);
    }

    /*private void Attack(AIStateController controller){
        RaycastHit hit;
        Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);

        if (Physics.SphereCast (controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange)
            && hit.collider.CompareTag ("Player")) 
        {
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD)){
                GameValues.instance.UpdateHealth(controller.enemyStats.damage);
                controller.stateTimeElapsed = 0;
            }
        }
    } OLD ATTACK FUNCTION*/
    private void Attack(AIStateController controller){
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) return;
        
        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD)){
                //GameValues.instance.UpdateHealth(controller.enemyStats.damage);
                Debug.Log("Attack");
                controller.stateTimeElapsed = 0;
            }
        }
    }
}
