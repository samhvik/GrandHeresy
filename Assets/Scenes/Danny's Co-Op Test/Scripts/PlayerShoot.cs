/*
    PlayerShoot.cs

    Handles player input related to weapon shooting.
    Uses new InputSystem with Callback Context
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour{

    // Character Controller GameObject
    private CharacterController controller;

    public WeaponSwitcher inventory;
    private Gun gun;

    void Awake(){
        // Grabbing our character controller that is on our player
        controller = gameObject.GetComponent<CharacterController>();
    }

    // OnFire fires our gun from our player
    public void OnFire()
    {
        gun.Fire();
    }

    // OnReload reloads our gun on our player
    public void OnReload()
    {
        gun.Reload();
    }

    void Update(){
    }

    public void SwitchWeapon(Gun newGun){
        this.gun = newGun;
    }
}
