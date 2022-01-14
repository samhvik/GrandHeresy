/*
    PlayerShooting.cs

    Handles player input related to weapon shooting.
*/

using UnityEngine;

public class PlayerShooting : MonoBehaviour{

    public WeaponSwitching inventory;
    private Gun gun;

    void Update(){
        if(Input.GetButtonDown("Reload"))
            gun.Reload();
        
        if(gun.IsSemi){
            if(Input.GetButtonDown("Shoot"))
                gun.Fire();
        }else{
            if(Input.GetButton("Shoot"))
                gun.Fire();
        }
        
    }

    public void SwitchWeapon(Gun newGun){
        this.gun = newGun;
    }
}
