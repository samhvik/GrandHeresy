/*
    PlayerShooting.cs

    Handles player input related to weapon shooting.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour{

    //public WeaponSwitching inventory;
    private PlayerInventory inventory;
    private Gun gun;
    private bool held = false;

    PlayerControls controls;


    void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>();
        controls = new PlayerControls();
    }

    void Update()
    {
        if (held)
        {
            if (gun.IsSemi)
            {
                gun.Fire();
                held = false;
            }
            else
            {
                gun.Fire();
            }
        }
    }

    // OnFire fires our gun from our player
    public void OnFire(InputAction.CallbackContext context)
    {
        // If pressed down (Sets true once)
        if (context.started)
        {
            held = true;
        }

        // If released (Sets false once)
        if (context.canceled)
        {
            held = false;
        }
    }

    // OnReload reloads our gun on our player
    public void OnReload(InputAction.CallbackContext context)
    {
        gun.Reload();
    }

    public void SwitchWeapon(Gun newGun)
    {
        this.gun = newGun;
    }

    // Enable and disable control input when script is enabled/disabled.
    public void OnEnable(){
        controls.Gameplay.Enable();
    }

    public void OnDisable(){
        controls.Gameplay.Disable();
    }

}
