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
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD)){
                // check if melee mob is in range
                if (controller.checkRange()){
                    controller.navMeshAgent.isStopped = true;
                    controller.StartCoroutine(MeleeAttack(fov.visibleTarget.gameObject, controller));
                    controller.stateTimeElapsed = -controller.enemyStats.attackCD;  //offset by how long im waiting to resume the attacking pause
                }
            }
        }
    }
    

    // delay AI movement below, defaul is wait half a second before turning the agent back on 
    IEnumerator MeleeAttack(GameObject target, AIStateController c){
        //c.GetComponent<Animator>().enabled = false; // Replace with melee animation transition
        c.animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(0.5f);
        
        // Play Sound
        c.aSource.clip = c.enemySounds[Random.Range(0, 1)];
        c.aSource.Play();
        // update the players health
        target.GetComponent<PlayerInventory>().UpdateHealth(c.enemyStats.damage);
        yield return new WaitForSeconds(0.25f); // following a target CD time
        c.navMeshAgent.isStopped = false; // let the AI walk again

        //c.GetComponent<Animator>().enabled = true; // Restart Walk Animation
        c.animator.SetBool("IsAttacking", false);
    }
}
