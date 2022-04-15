using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Special Attack")]
public class AISpecialAttack: AIAction
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private ParticleSystem AoEParticleSet;
    public override void Act(AIStateController controller){
        Attack(controller);
    }

    private void Attack(AIStateController controller){
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) return;

        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){ // visible target found
            // do special attack IF possible
            // controller.enemyStats.specialCD != 0 && 
            if(controller.CheckIfCountdownElapse(controller.enemyStats.specialCD)){
                controller.navMeshAgent.isStopped = true;
                //Debug.Log("Starting to Cast...");
                // update the players health & attack
                controller.StartCoroutine(attackingPause(fov.visibleTarget.gameObject, controller));
                controller.stateTimeElapsed = -controller.enemyStats.specialCD; // reset time elapsed
            }

            /* basic attack?
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD) && controller.checkRange()){
                //Stop Controller from Moving briefly for animations
                controller.navMeshAgent.isStopped = true;
                //Debug.Log("Attack Animation Here");
                //Debug.Log("Attack Sound Here");
                Debug.Log("Play Attack Sound Here");
                // update the players health
                fov.visibleTarget.gameObject.GetComponent<PlayerInventory>().UpdateHealth(controller.enemyStats.damage);
            }*/
        }
    }

    IEnumerator attackingPause(GameObject tar, AIStateController c){
        // instantiate particle system
        // GameObject AoE = Instantiate(AoEParticleSet, tar.transform.position, Quaternion.identity);
        Destroy(Instantiate(AoEParticleSet, tar.transform.position, Quaternion.identity), 1); // destroy after cast
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Attack Animation Here && Draw Circle on Ground");
        // only hit players and update health
        Collider[] hits = Physics.OverlapSphere(tar.transform.position, 3, PlayerLayer);
        foreach(Collider h in hits){ h.GetComponent<PlayerInventory>().UpdateHealth(c.enemyStats.damage);  
        Debug.Log("Hit: " + h.GetComponent<PlayerInventory>().health);}

        yield return new WaitForSeconds(0.25f);
        // destroy particle system
        // Destroy(AoE);
        c.navMeshAgent.isStopped = false; // let the AI walk again
    }
}

// Projectile Ranged Attack Issues
// using a bullet since the script is already made, altering the "round" script to detect when a bullet hits a "Player"
// would induce friendly fire into the game. a way around that?
// IDEAS
// copy paste round to be something else and instantiate as that

// ???