/*
    WeaponSwitching.cs

    Handles the inventory system for weapons.
    Supposed to be attached to the inventory game object.
*/

using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int inventorySize;
    private Gun currentWeapon;
    public PlayerShooting shooter;

    void Start(){
        SelectWeapon();
    }

    
    void Update(){
        if(Input.GetButtonDown("SwitchWeapon")){
            int previousSelectedWeapon = selectedWeapon;

            selectedWeapon = (selectedWeapon + 1) % inventorySize;

            if(previousSelectedWeapon != selectedWeapon){
                SelectWeapon();
            }
        }
    }

    public void SelectWeapon(){
        int index = 0;
        foreach(Transform weapon in transform){
            if(index == selectedWeapon){
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.gameObject.GetComponent<Gun>();
                shooter.SwitchWeapon(currentWeapon);
            }
            else{
                weapon.gameObject.SetActive(false);
            }
            index++;
        }
    }

    public Gun CurrentWeapon{
        get{
            return currentWeapon;
        }
    }
}
