using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private VoiceLines voiceLines;
    private PlayerInventory player;
    public Animator am;

    private float friendlyDamageModifier;
    
    void Start(){
        am = this.GetComponent<Animator>();
        voiceLines = this.GetComponent<VoiceLines>();
        player = this.GetComponent<PlayerInventory>();
        friendlyDamageModifier = 0.33f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnCollisionEnter(Collision other){
        if(other.gameObject.CompareTag("PlayerProjectile")){
            FriendlyFire(other.gameObject.GetComponent<Round>().damage);
        }else{
            //TakeDamage(other.gameObject).GetComponent<EnemyAttackThing>().damage);
        }
    }*/

    public void FriendlyFire(float damage){
        player.UpdateHealth((int)(damage * friendlyDamageModifier));
        am.SetTrigger("PlayerHit");
        voiceLines.FriendlyFire();
    }

    public void TakeDamage(float damage){
        player.UpdateHealth((int)damage);
        am.SetTrigger("PlayerHit");
        voiceLines.RecieveDamage();
    }
}
