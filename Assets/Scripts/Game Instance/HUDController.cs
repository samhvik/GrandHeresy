/*
    HUDController.cs

    Handles all relevant HUD info. Mainly used for debugging at the moment.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text ammoText;

    public Text playerMovementStateText;
    private PlayerMovement movement;

    public Text currentWeaponText;
    private WeaponSwitching inventory;

    void Start()
    {
        movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        inventory = GameObject.Find("WeaponInventory").GetComponent<WeaponSwitching>();
    }

    void LateUpdate()
    {
        switch(inventory.CurrentWeapon.shootState){
            case Gun.ShootState.Reloading:
                ammoText.text = "Reloading...";
                break;
            default:
                ammoText.text = inventory.CurrentWeapon.CurrentAmmo + "/" + inventory.CurrentWeapon.MaxAmmo;
                break;
        }

        playerMovementStateText.text = "Movement State: " + movement.currentState;

        currentWeaponText.text = "" + inventory.CurrentWeapon.Name;
    }
}
