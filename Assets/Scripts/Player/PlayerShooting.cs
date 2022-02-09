/*
    PlayerShooting.cs

    Handles player input related to weapon shooting.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour{

    public bool held = false;
    public WeaponSwitching inventory;
    private Gun gun;

    PlayerControls controls;

    void Awake(){
        controls = new PlayerControls();
        
        // Callback function for shooting. ctx is the context handler.
        controls.Gameplay.Shoot.started += ctx => ShootPressed(true);
        controls.Gameplay.Shoot.canceled += ctx => ShootPressed(false);

        // Callback function for reloading
        controls.Gameplay.Reload.performed += ctx => Reload();
    }

    // Enable and disable control input when script is enabled/disabled.
    public void OnEnable(){
        controls.Gameplay.Enable();
    }

    public void OnDisable(){
        controls.Gameplay.Disable();
    }

    void Update(){
        if(held){
            if(gun.IsSemi){
                gun.Fire();
                held = false;
            }else{
                gun.Fire();
            }
        }
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

    private void ShootPressed(bool set){
        held = set;
    }
}
