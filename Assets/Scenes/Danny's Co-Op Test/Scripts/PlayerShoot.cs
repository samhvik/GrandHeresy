/*
    PlayerShoot.cs

    Handles player input related to weapon shooting.
    Uses new InputSystem with Callback Context
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour{

    public WeaponSwitcher inventory;
    private Gun gun;
    private bool held = false;

    void Awake(){
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
        if(context.started)
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
    public void OnReload()
    {
        gun.Reload();
    }

    public void SwitchWeapon(Gun newGun){
        this.gun = newGun;
    }
}
