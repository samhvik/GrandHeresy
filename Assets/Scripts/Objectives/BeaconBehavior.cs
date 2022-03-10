using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject redRing;
    public GameObject blueRing;
    public GameObject greenRing;
    public Collider playerCollider;

    private bool allPlayersIn = false;
    private bool extractionAvailable = false;
    void Start()
    {
        blueRing.SetActive(false);
        greenRing.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(IsReached()){
           extractionAvailable = true;
       }
       updateRing();
    }    
 
    //called when something enters the trigger
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders () { return colliders; }
 
     private void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") { 
            colliders.Add(other); 
        }
     }
 
     private void OnTriggerExit (Collider other) {
         colliders.Remove(other);
     }
     private bool checkAllPlayersInRange(){ 
         int count = 0;
         foreach(Collider col in colliders){
             count++;
         }
         if(count == GameValues.instance.numPlayers){
             allPlayersIn = true;
             GameValues.instance.allPlayersInExtractionRange = true;
         }
         else{
             allPlayersIn = false;
             GameValues.instance.allPlayersInExtractionRange = false;

         }
         return allPlayersIn;
     }
    private void updateRing(){
        if(extractionAvailable){
            redRing.SetActive(false);
            checkAllPlayersInRange();
            if(allPlayersIn){
                print("greenRing is active");
                blueRing.SetActive(false);
                greenRing.SetActive(true);
            }
            else{
                print("blueRing is active");
                greenRing.SetActive(false);
                blueRing.SetActive(true);
            }
        }
        else{
            print("redRing is active");
            redRing.SetActive(true);
        }
    }
    private bool IsReached()
    {
        return (GameValues.instance.objectivesCompleted >= GameValues.instance.objectivesTotal);
    }
}
