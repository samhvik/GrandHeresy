using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInventory : MonoBehaviour {
    public int inventorySize;
    public Gun starterGun;
    //public Transform gunHandle; 
    public GameObject gunPosition;
    public float health = 100.0f;
    
    private List<Gun> guns = new List<Gun>();
    private PlayerShooting shooter;
    private int head = 0;
    private int kills = 0;

    public Animator am;
    // public Renderer playerRenderer;
    // public Material lerpMaterial;
    // public float lerpTime;
    // private Material originalMaterial;
    
    PlayerControls controls;
    
    
    // Start is called before the first frame update
    void Start() {
        //gunHandle = starterGun.gameObject.transform.GetChild(0);
        var gun = Instantiate(starterGun, gunPosition.transform);

        //gun.transform

        gun.setInventory(this);
        gun.tag = "Gear";
        guns.Add(gun);
        
        //what the fuck
        shooter = GetComponent<PlayerShooting>();
        shooter.SwitchWeapon(guns[head]);

        //originalMaterial = playerRenderer.material;
    }

    void Awake(){
        am = this.GetComponent<Animator>();
        controls = new PlayerControls();
    }
    
    // Enable and disable control input when script is enabled/disabled.
    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }

    // When button to pickup weapon is used
    // Only called on gear with drop tag, so all will have Gun component
    public void OnPickupWeapon(Collider drop) {
        drop.tag = "Gear";
        drop.transform.parent = gunPosition.transform;
        drop.transform.position = gunPosition.transform.position; 
        drop.transform.forward = gunPosition.transform.forward; 
        drop.gameObject.SetActive(false);

        drop.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        drop.gameObject.GetComponent<FlashMaterialInRange>().enabled = false;

        var gun = drop.gameObject.GetComponent<Gun>();
        gun.setInventory(this);
        guns.Add(gun);
    }

    public int getHead() {
        return head;
    }


    public int getKills() {
        return kills;
    }

    public void addKills() {
        kills++;
    }

    public int incrementHead() {
        head = (head + 1) % guns.Count;
        return head;
    }
    public Gun CurrentWeapon {
        get { return guns[head]; }
    }

    public int InventoryCurrentSize() {
        return guns.Count;
    }

    public Gun getGunAtIndex(int index) {
        return guns[index];
    }

    // update health 
    public void UpdateHealth(int damage){
        am.ResetTrigger("PlayerHit");
        am.SetTrigger("PlayerHit");
        //playerRenderer.material.Lerp(originalMaterial, lerpMaterial, 2);
        health -= damage;
    }
}
