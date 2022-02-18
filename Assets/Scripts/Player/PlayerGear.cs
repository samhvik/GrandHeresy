using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerGear : MonoBehaviour {
    public int inventorySize;
    public Gun starterGun;
    public float pickupRadius;
    
    private List<Gun> guns = new List<Gun>();
    private PlayerShooting shooter;
    private int head = 0;
    
    PlayerControls controls;
    
    
    // Start is called before the first frame update
    void Start() {
        var gun = Instantiate(starterGun,transform);
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

    // When button to swap weapons is used
    public void OnSwapWeapons(InputAction.CallbackContext context) {
        if (guns.Count > 1) {
            guns[head].gameObject.SetActive(false);
            head = (head + 1) % Math.Min(inventorySize,guns.Count);
            guns[head].gameObject.SetActive(true);
            shooter.SwitchWeapon(guns[head]);
        }
    }

    // When button to pickup weapon is used
    public void OnPickupWeapon(InputAction.CallbackContext context) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (var drop in colliders) {
            if (drop.CompareTag("Drop")) {
                
                drop.tag = "Gear";
                drop.transform.parent = transform;
                drop.transform.position = transform.position;
                drop.transform.forward = transform.forward;
                drop.gameObject.SetActive(false);
                guns.Add(drop.gameObject.GetComponent<Gun>());
            }
        }
    }
    
    public Gun CurrentWeapon {
        get { return guns[head]; }
    }
}
