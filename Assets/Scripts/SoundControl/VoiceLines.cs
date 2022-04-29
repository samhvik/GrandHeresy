using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceLines : MonoBehaviour
{
    public AudioClip[] proceedOrdersNormal;
    public AudioClip[] proceedOrdersStress;
    // ------------------------------------
    public AudioClip[] retreatOrdersNormal;
    public AudioClip[] retreatOrdersStress;
    // ------------------------------------
    public AudioClip[] helpRequestsNormal;
    public AudioClip[] helpRequestsStress;
    // ------------------------------------
    public AudioClip[] battleCriesNormal;
    public AudioClip[] battleCriesStress;
    // ------------------------------------
    public AudioClip[] recieveDamageNormal;
    public AudioClip[] recieveDamageStress;
    // ------------------------------------
    public AudioClip[] friendlyFireNormal;
    public AudioClip[] friendlyFireStress;
    // ------------------------------------
    public AudioClip[] reloadNormal;
    public AudioClip[] reloadStress;
    // ------------------------------------
    public AudioClip[] deploymentNormal;
    // ------------------------------------
    public AudioClip[] objectiveCompleteNormal;
    public AudioClip[] objectiveCompleteStress;
    // ------------------------------------
    public AudioClip[] tauntNormal;
    public AudioClip[] tauntStress;
    // ------------------------------------
    private AudioSource source;

    private float pitchMultiplier = 0.08f;

    //private PlayerControls controls;

    private float cooldown = 0f;

    void Awake(){
        /*controls = new PlayerControls();
        controls.Gameplay.ProceedOrder.performed += ctx => ProceedOrder();
        controls.Gameplay.RetreatOrder.performed += ctx => RetreatOrder();
        controls.Gameplay.RequestHelp.performed += ctx => RequestHelp();
        controls.Gameplay.BattleCry.performed += ctx => BattleCry();*/
    }

    // Update will handle the cooldown depletion and make sure it doesnt become negative.
    void Update(){
        if(cooldown > 0){
            cooldown -= Time.deltaTime;
        }

        if(cooldown < 0)
            cooldown = 0;
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Chance to not say anything at all
    private bool NoTalk(){
        float chance = Random.Range(0f, 10f);
        if(chance <= 4f || cooldown > 0f)
            return true;
        return false;
    }

    private bool NoTalk(int added){
        float chance = Random.Range(0f, 10f);
        if(chance <= 4f + added || cooldown > 0f)
            return true;
        return false;
    }

    // Are the players not in combat?
    private bool IsNormal(){
        return !GameValues.inCombatStatus;
    }

    // Randomize the pitch to make it more procedural
    private void RandomizeSound(){
        source.pitch = Random.Range(1-pitchMultiplier, 1+pitchMultiplier);
        //source.pitch = 0.9f;
    }

    // Play the voice line and trigger a cooldown
    private void PlayLine(){
        source.Play();
        cooldown = 3f;
    }

    private bool CooldownCheck(){
        return !(cooldown == 0);
    }

    private void PlayLineNoCool(){
        source.Play();
    }

    public void ProceedOrder(){
        if(CooldownCheck())
            return;
        
        if(IsNormal()){
            source.clip = proceedOrdersNormal[Random.Range(0, proceedOrdersNormal.Length)];
        }else{
            source.clip = proceedOrdersStress[Random.Range(0, proceedOrdersStress.Length)];
        }
        source.pitch = Random.Range(1-pitchMultiplier, 1+pitchMultiplier);
        //source.PlayOneShot(source.clip);
        PlayLine();
        Debug.Log("ProceedOrder");
    }

    public void RetreatOrder(){
        if(CooldownCheck())
            return;
        
        if(IsNormal()){
            source.clip = retreatOrdersNormal[Random.Range(0, retreatOrdersNormal.Length)];
        }else{
            source.clip = retreatOrdersStress[Random.Range(0, retreatOrdersStress.Length)];
        }
        RandomizeSound();
        //source.PlayOneShot(source.clip);
        PlayLine();
        Debug.Log("RetreatOrder");
    }

    public void RequestHelp(){
        if(CooldownCheck())
            return;
        
        if(IsNormal()){
            source.clip = helpRequestsNormal[Random.Range(0, helpRequestsNormal.Length)];
        }else{
            source.clip = helpRequestsStress[Random.Range(0, helpRequestsStress.Length)];
        }
        RandomizeSound();        //source.PlayOneShot(source.clip);
        PlayLine();
        Debug.Log("RequestAssistance");
    }

    public void BattleCry(){
        if(CooldownCheck())
            return;
        
        if(IsNormal()){
            source.clip = battleCriesStress[Random.Range(0, battleCriesStress.Length)]; //Will use normal when line is recorded.
        }else{
            source.clip = battleCriesStress[Random.Range(0, battleCriesStress.Length)];
        }
        RandomizeSound();
        //source.PlayOneShot(source.clip);
        PlayLine();
        Debug.Log("BattleCry");
    }

    public void RecieveDamage(){
        if(CooldownCheck())
            return;

        if(NoTalk())
            return;
        
        if(IsNormal()){
            source.clip = recieveDamageNormal[Random.Range(0, recieveDamageNormal.Length)];
        }else{
            source.clip = recieveDamageStress[Random.Range(0, recieveDamageStress.Length)];
        }
        RandomizeSound();
        PlayLine();
    }

    public void FriendlyFire(){
        if(CooldownCheck())
            return;

        if(IsNormal()){
            source.clip = friendlyFireNormal[Random.Range(0, friendlyFireNormal.Length)];
        }else{
            source.clip = friendlyFireStress[Random.Range(0, friendlyFireStress.Length)];
        }
        RandomizeSound();
        PlayLine();
    }
    public void Reload(){
        if(CooldownCheck())
            return;

        if(NoTalk(3))
            return;
        
        if(IsNormal()){
            source.clip = reloadNormal[Random.Range(0, reloadNormal.Length)];
        }else{
            source.clip = reloadStress[Random.Range(0, reloadStress.Length)];
        }
        RandomizeSound();
        PlayLine();
    }

    public void Deployment(){
        if(CooldownCheck())
            return;

        if(NoTalk())
            return;
        
        source.clip = deploymentNormal[Random.Range(0, deploymentNormal.Length)];
        
        RandomizeSound();
        PlayLine();
    }

    public void ObjectiveComplete(){
        if(CooldownCheck())
            return;

        if(NoTalk())
            return;
        
        if(IsNormal()){
            source.clip = objectiveCompleteNormal[Random.Range(0, objectiveCompleteNormal.Length)];
        }else{
            source.clip = objectiveCompleteStress[Random.Range(0, objectiveCompleteStress.Length)];
        }
        RandomizeSound();
        PlayLine();
    }

    public void Taunt(){
        if(CooldownCheck())
            return;

        if(NoTalk())
            return;
        
        if(IsNormal()){
            source.clip = tauntNormal[Random.Range(0, tauntNormal.Length)];
        }else{
            source.clip = tauntStress[Random.Range(0, tauntStress.Length)];
        }
        RandomizeSound();
        PlayLine();
    }
}
