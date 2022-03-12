/*
    Round.cs

    The object spawned when as gun is fired.
    References the source gun to access ballistics info.
*/

using System;
using UnityEngine;

public class Round : MonoBehaviour
{
    public GameObject blood;
    public float damage;                    // how much enemy HP is reduced by on impact
    public float despawnTime = 1.5f;        // travel time in seconds before despawning
    private float counter = 0f;
    public bool overPenetrate = false;      // determines if bullet will go through targets
    private PlayerInventory inventory;

    void Update(){
        counter += Time.deltaTime;
        if(counter > despawnTime)
            Destroy(this.gameObject);
    }

    public void updateInventory(PlayerInventory inventory) {
        this.inventory = inventory;
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Target"){
            var target = other.gameObject.GetComponent<Target>();
            GameObject gust = Instantiate(
                    blood,
                    transform.position,
                    transform.rotation
                );

            target.Hit(damage);
            if (target.health <= 0) {
                inventory.addKills();
                Debug.Log("Kills: " + inventory.getKills());
            }
            

            // Destroy this round if not overpenetratable 
            if(!overPenetrate)
                Destroy(this.gameObject);
        }
        
        if(other.gameObject.tag == "Rune"){
            other.gameObject.GetComponent<RuneObjective>().Hit(damage);

            // Destroy this round if not overpenetratable 
            if(!overPenetrate)
                Destroy(this.gameObject);
        }
    }
}
