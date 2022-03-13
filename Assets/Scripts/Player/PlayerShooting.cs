/*
    PlayerShooting.cs

    Handles player input related to weapon shooting.
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour{

    //public WeaponSwitching inventory;
    private PlayerInventory inventory;
    private AnimatorManager animManager;
    private Gun gun;
    private bool held = false;

    PlayerControls controls;


    void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>();
        animManager = this.GetComponent<AnimatorManager>();
        controls = new PlayerControls();
    }

    void Update()
    {
        if (held)
        {
            if(gun!=null){
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
        if(context.performed){
            Debug.Log("Reloading...");
            gun.Reload();
            animManager.TriggerReload();
        }
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
