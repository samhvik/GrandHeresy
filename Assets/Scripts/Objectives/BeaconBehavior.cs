using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public bool playerInCollider = false; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // print(GetColliders());
    }

    private HashSet<Collider> colliders = new HashSet<Collider>();
 
    public HashSet<Collider> GetColliders () { return colliders; }
  
    private void OnTriggerEnter (Collider other) {
          colliders.Add(other); //hashset automatically handles duplicates
          print(other.tag + "this just entered the beacon range");
          if(other.tag =="Player"){
              playerInCollider = true; 
          }
    }
  
    private void OnTriggerExit (Collider other) {
          colliders.Remove(other);
           print(other.tag + "this just left the beacon range");
          if(other.tag =="Player"){
              playerInCollider = false; 
          }
    }
}
