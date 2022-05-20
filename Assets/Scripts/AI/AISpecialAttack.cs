using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Special Attack")]
public class AISpecialAttack: AIAction
{   
    [SerializeField] private GameObject SpikeObject;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private GameObject AoEParticleSet;
    

    public override void Act(AIStateController controller){
        Attack(controller);
    }

    private void Attack(AIStateController controller){
        AIFOV fov = controller.GetComponent<AIFOV>();
        if(fov == null) return;

        if(fov.visibleTarget != null && fov.visibleTarget.CompareTag("Player")){ // visible target found
            if(controller.CheckIfCountdownElapse(controller.enemyStats.specialCD)){
                // if within "melee" distance we choose to melee not cast, and reset state time to smaller amount
                controller.navMeshAgent.isStopped = true;
                if (Vector3.Distance(fov.visibleTarget.position, controller.transform.position) <= 5f){
                    controller.StartCoroutine(MeleeAttack(fov.visibleTarget.gameObject, controller));
                    controller.stateTimeElapsed = -controller.enemyStats.attackCD; // reset time elapsed
                } else {
                    Vector3 location = fov.visibleTarget.position;
                    location.y = -19;
                    controller.StartCoroutine(RangedAttack(location, controller));
                    controller.stateTimeElapsed = -controller.enemyStats.specialCD; // reset time elapsed
                }
            }
        }
    }

    IEnumerator RangedAttack(Vector3 locale, AIStateController c){
        var particle = Instantiate(AoEParticleSet, locale, Quaternion.Euler(90, 0, 0));
        var obj = Instantiate(SpikeObject, locale, Quaternion.Euler(0, 0, 0)); // destroy after cast
        c.animator.SetBool("IsAttacking", true);
        // play a casting sounds here if avail
        
        yield return new WaitForSeconds(1.5f); // Cast time

        // only hit players and update health
        Collider[] hits = Physics.OverlapSphere(locale, 3, PlayerLayer);
        obj.GetComponentInChildren<Animator>().SetBool("TriggerSpikes", true);
        obj.transform.position = locale;
        //Debug.Log(hits);
        foreach(Collider h in hits){ h.GetComponent<PlayerInventory>().UpdateHealth(c.enemyStats.damage); }
        
        yield return new WaitForSeconds(0.75f); // following a target CD time

        Destroy(obj);
        Destroy(particle);
        c.navMeshAgent.isStopped = false; // let the AI walk again
        c.animator.SetBool("IsAttacking", false);
        obj.GetComponentInChildren<Animator>().SetBool("TriggerSpikes", false); // restart walk animation cycle
    }
    IEnumerator MeleeAttack(GameObject target, AIStateController c){
        c.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        // Play Sound
        c.aSource.clip = c.enemySounds[Random.Range(0, 1)];
        c.aSource.Play();
        // update the players health
        target.GetComponent<PlayerInventory>().UpdateHealth(c.enemyStats.damage);
        yield return new WaitForSeconds(0.25f); // following a target CD time
        c.navMeshAgent.isStopped = false; // let the AI walk again
        c.GetComponent<Animator>().enabled = true; // Restart Walk Animation
    }
}