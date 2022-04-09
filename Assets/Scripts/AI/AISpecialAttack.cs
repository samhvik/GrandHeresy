using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Special Attack")]
public class AISpecialAttack: AIAction
{
    public override void Act(AIStateController controller){
        Attack(controller);
    }

    private void Attack(AIStateController controller){
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) return;

        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){ // visible target found
            // do special attack
            // if controller.enemyStats.SpecialCD != 0 && controller.CheckIfCountdownElapse(controller.enemyStats.SpecialCD)
            // enemy STOPS moving for the entire attack
            // controller.StartCoroutine(attackingPause(controller));
            // then do the attack here   

            // basic attack
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD) && controller.checkRange()){
                //Stop Controller from Moving briefly for animations
                controller.navMeshAgent.isStopped = true;
                //Debug.Log("Attack Animation Here");
                //Debug.Log("Attack Sound Here");
                Debug.Log("Play Attack Sound Here");
                // update the players health
                fov.visibleTarget.gameObject.GetComponent<PlayerInventory>().UpdateHealth(controller.enemyStats.damage);
                controller.StartCoroutine(attackingPause(controller));
            }
        }
    }

    // delay AI movement below, defaul is wait half a second before turning the agent back on 
    IEnumerator attackingPause(AIStateController controller){
        yield return new WaitForSeconds(1f);
        controller.navMeshAgent.isStopped = false;
    }
}
