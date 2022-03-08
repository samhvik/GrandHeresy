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
    // Holds Text for weapons, ammo, and state for debugging
    public Text[] weaponText = new Text[4];
    public Text[] ammoText = new Text[4];
    public Text[] stateText = new Text[4];

    // Holds our players inventory
    private PlayerInventory inventory;

    void Awake()
    {
        // Disable all text to begin with
        for (int i = 0; i < 4; i++)
        {
            weaponText[i].gameObject.SetActive(false);
            ammoText[i].gameObject.SetActive(false);
            stateText[i].gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        // Enabling Text to Display and Update Text
        for (int i = 0; i < GameValues.instance.getNumPlayers(); i++)
        {
            PlayerInventory inventory = GameValues.instance.getPlayer(i).GetComponentInChildren<PlayerInventory>(); // Danny: Change when implementing to main


            // Set the weapon text
            weaponText[i].gameObject.SetActive(true);
            weaponText[i].text = "" + inventory.CurrentWeapon.Name; // Danny: Change when implementing to main

            // Set the ammo text
            ammoText[i].gameObject.SetActive(true);
            switch (inventory.CurrentWeapon.shootState) // Danny: Change when implementing to main
            {
                case Gun.ShootState.Reloading:
                    ammoText[i].text = "Reloading...";
                    break;
                default:
                    ammoText[i].text = inventory.CurrentWeapon.CurrentAmmo + "/" + inventory.CurrentWeapon.MaxAmmo;
                    break;
            }

            // Set the state text
            stateText[i].gameObject.SetActive(true);
            stateText[i].text = "Movement State: " + GameValues.instance.getPlayer(i).GetComponent<MovementSM>().currentState; 
        }
    }
}
