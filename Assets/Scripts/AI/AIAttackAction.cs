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
                //Stop Controller from Moving briefly for animations
                controller.navMeshAgent.isStopped = true;
                //Debug.Log("Attack Animation Here");
                //Debug.Log("Attack Sound Here");
                Debug.Log("Play Attack Sound Here");
                // update the players health
                fov.visibleTarget.gameObject.GetComponent<PlayerInventory>().UpdateHealth(controller.enemyStats.damage);
                controller.StartCoroutine(attackingPause(controller));
                controller.stateTimeElapsed = -controller.enemyStats.attackCD;  //= 0  offset by how long im waiting to resume the attacking pause
            }
        }
    }

    // delay AI movement below, defaul is wait half a second before turning the agent back on 
    IEnumerator attackingPause(AIStateController controller){
        yield return new WaitForSeconds(0.5f);
        controller.navMeshAgent.isStopped = false;
    }
}
