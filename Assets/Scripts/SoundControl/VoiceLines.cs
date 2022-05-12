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

    // Holds the Prefab for Voice Text
    public GameObject voiceText;
    // Holds A List of Voice Text's
    private List<GameObject> voiceList = new List<GameObject>();

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

        // Moves Text Upwards and Eventually Deletes
        for(int i = 0; i < voiceList.Count; i++)
        {
            voiceList[i].transform.position = new Vector3(this.transform.position.x, voiceList[i].transform.position.y + 0.01f, this.transform.position.z);

            // Slowly Fades out Text
            Color alphaChanger = voiceList[i].GetComponent<TextMesh>().color;
            alphaChanger.a -= 0.002f;
            voiceList[i].GetComponent<TextMesh>().color = alphaChanger;

            if(voiceList[i].transform.position.y >= -7.5f)
            {
                Destroy(voiceList[i]);
                voiceList.Remove(voiceList[i]);
            }
        }
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Move Move Move!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Fall Back!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Need Help!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Lets Die Trying!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Damn It!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Watch your Fire!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Reloading!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
    }

    public void Deployment(){
        if(CooldownCheck())
            return;

        if(NoTalk())
            return;
        
        source.clip = deploymentNormal[Random.Range(0, deploymentNormal.Length)];
        
        RandomizeSound();
        PlayLine();

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "What a Hell Hole...!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Completed!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
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

        chooseVoiceTextColor();
        voiceText.GetComponent<TextMesh>().text = "Hell Scum!";
        GameObject toInsert = Instantiate(voiceText, new Vector3(this.transform.position.x, this.transform.position.y + 7f, this.transform.position.z), Quaternion.Euler(30f, 0f, 0f));
        voiceList.Add(toInsert);
    }

    // Choose the Color of the Player
    public void chooseVoiceTextColor()
    {
        // Setting Color of Voicelines based on Players Color
        if (this.GetComponent<PlayerInputHandler>().getPlayerIndex() == 0)
        {
            voiceText.GetComponent<TextMesh>().color = Color.red;
        }
        else if (this.GetComponent<PlayerInputHandler>().getPlayerIndex() == 1)
        {
            voiceText.GetComponent<TextMesh>().color = Color.green;
        }
        else if (this.GetComponent<PlayerInputHandler>().getPlayerIndex() == 2)
        {
            voiceText.GetComponent<TextMesh>().color = Color.blue;
        }
        else if (this.GetComponent<PlayerInputHandler>().getPlayerIndex() == 3)
        {
            voiceText.GetComponent<TextMesh>().color = Color.yellow;
        }
    }
}
