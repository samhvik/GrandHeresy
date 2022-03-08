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
    public GameObject gunPosition;
    
    private List<Gun> guns = new List<Gun>();
    private PlayerShooting shooter;
    private int head = 0;
    
    PlayerControls controls;
    
    
    // Start is called before the first frame update
    void Start() {
        var gun = Instantiate(starterGun,gunPosition.transform);
        gun.tag = "Gear";
        guns.Add(gun);
        
        //what the fuck
        shooter = GetComponent<PlayerShooting>();
        shooter.SwitchWeapon(guns[head]);
    }

    void Awake(){
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
        guns.Add(drop.gameObject.GetComponent<Gun>());
    }

    public int getHead() {
        return head;
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
}
