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
                controller.StartCoroutine(attackingPause(fov.visibleTarget.gameObject, controller));
                controller.stateTimeElapsed = -controller.enemyStats.attackCD;  //= 0  offset by how long im waiting to resume the attacking pause
            }
        }
    }

    // delay AI movement below, defaul is wait half a second before turning the agent back on 
    IEnumerator attackingPause(GameObject target, AIStateController c){
        yield return new WaitForSeconds(0.25f);
        // Play Animation Here
        // Lunge Idea: transform.Translate ((lungeSpeed + lungeDistance, lungeHeight, 0)*Time.deltaTime)
        // a yiled return wait
        // Play Sound
        c.aSource.clip = c.enemySounds[Random.Range(0, 1)];
        c.aSource.Play();
        yield return new WaitForSeconds(0.25f); // following a target CD time
        // update the players health
        target.GetComponent<PlayerInventory>().UpdateHealth(c.enemyStats.damage);
        c.navMeshAgent.isStopped = false; // let the AI walk again
    }
}
