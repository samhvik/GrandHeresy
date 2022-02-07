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
    private bool held = false;


    void Awake(){
        // Grabbing our character controller that is on our player
        controller = gameObject.GetComponent<CharacterController>();
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
        if (context.ReadValue<float>() > 0.3f)
        {
              held = true;
        }
        else
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
