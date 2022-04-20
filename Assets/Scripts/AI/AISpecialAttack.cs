using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Special Attack")]
public class AISpecialAttack: AIAction
{
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private GameObject AoEParticleSet;
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
                Vector3 location = fov.visibleTarget.gameObject.transform.position;
                location.y = -19;
                controller.StartCoroutine(attackingPause(location, controller));
                controller.stateTimeElapsed = -controller.enemyStats.specialCD; // reset time elapsed
            }

            /* basic attack?
            if(controller.CheckIfCountdownElapse(controller.enemyStats.attackCD) && controller.checkRange()){
                //Stop Controller from Moving briefly for animations
                controller.navMeshAgent.isStopped = true;
                // update the players health
                fov.visibleTarget.gameObject.GetComponent<PlayerInventory>().UpdateHealth(controller.enemyStats.damage);
            }*/
        }
    }

    IEnumerator attackingPause(Vector3 locale, AIStateController c){
        Destroy(Instantiate(AoEParticleSet, locale, Quaternion.Euler(90, 0, 0)), 2); // destroy after cast
        yield return new WaitForSeconds(1.5f); // Cast time
        //Debug.Log("Attack Animation Here && Draw Circle on Ground");
        //c.aSource.clip = c.enemySounds[Random.Range(0, c.enemySounds.Length-1)];
        //c.aSource.Play();
        // only hit players and update health
        Collider[] hits = Physics.OverlapSphere(locale, 3, PlayerLayer);
        //Debug.Log(hits);
        foreach(Collider h in hits){ h.GetComponent<PlayerInventory>().UpdateHealth(c.enemyStats.damage); }

        yield return new WaitForSeconds(0.25f); // following a target CD time
        c.navMeshAgent.isStopped = false; // let the AI walk again
    }
}

// Projectile Ranged Attack Issues
// using a bullet since the script is already made, altering the "round" script to detect when a bullet hits a "Player"
// would induce friendly fire into the game. a way around that?
// IDEAS
// copy paste round to be something else and instantiate as that
// ???