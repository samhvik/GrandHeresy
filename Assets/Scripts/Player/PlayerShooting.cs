/*
    PlayerShooting.cs

    Handles player input related to weapon shooting.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour{

    public WeaponSwitching inventory;
    private Gun gun;

    PlayerControls controls;

    void Awake(){
        controls = new PlayerControls();
        
        // Callback function for shooting. ctx is the context handler.
        controls.Gameplay.Shoot.performed += ctx => Fire();

        // Callback function for reloading
        controls.Gameplay.Reload.performed += ctx => Reload();
    }

    // Enable and disable control input when script is enabled/disabled.
    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }

    void Update(){
        /*if(Input.GetButtonDown("Reload"))
            gun.Reload();
        
        if(gun.IsSemi){
            if(Input.GetButtonDown("Shoot"))
                gun.Fire();
        }else{
            if(Input.GetButton("Shoot"))
                gun.Fire();
        }*/
        
    }

    private void Fire(){
        gun.Fire();
    }

    private void Reload(){
        gun.Reload();
    }

    public void SwitchWeapon(Gun newGun){
        this.gun = newGun;
    }
}
