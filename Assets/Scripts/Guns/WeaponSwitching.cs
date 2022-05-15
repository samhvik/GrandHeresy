/*
    WeaponSwitching.cs

    Handles the inventory system for weapons.
    Supposed to be attached to the inventory game object.
*/

using UnityEngine;
using System.Collections.Generic;

public class WeaponSwitching : MonoBehaviour {
  private Gun currentWeapon;
  private PlayerShooting shooter;
  private PlayerInventory inventory;
  public AnimatorManager animatorManager;
  PlayerControls controls;
  private List<Gun> guns;

  void Start() {
  }

  void Awake() {
    inventory = GetComponent<PlayerInventory>();
    animatorManager = GetComponent<AnimatorManager>();
    shooter = gameObject.GetComponent<PlayerShooting>();
    controls = new PlayerControls();
    // callback function for the reload button
    controls.Gameplay.SwapWeapons.performed += ctx => Handle();
  }

  // Enable and disable control input when script is enabled/disabled.
  void OnEnable() {
    controls.Gameplay.Enable();
  }

  void OnDisable() {
    controls.Gameplay.Disable();
  }

  private void Handle() {
    //Debug.Log("I AM HERE OH YEA");
    int prevWeaponIndex = inventory.getHead();
    int newWeaponIndex = inventory.incrementHead();
    if (prevWeaponIndex != newWeaponIndex) {
      //Debug.Log("WE FUCKING SELECTING WOOOOOOO");
      SelectWeapon();
      if(currentWeapon.name == "P-2416 Apostle"){
        animatorManager.HandlePistolState(true);
      }
      else{
        animatorManager.HandlePistolState(false);
      }
    }
  }

  private void SelectWeapon() {
    int weaponIndex = inventory.getHead();
    int inventoryCurrentSize = inventory.InventoryCurrentSize();
    int prevWeaponIndex = (weaponIndex + inventoryCurrentSize - 1) % inventoryCurrentSize;
    currentWeapon = inventory.getGunAtIndex(weaponIndex);
    currentWeapon.gameObject.SetActive(true);
    shooter.SwitchWeapon(currentWeapon);
    inventory.getGunAtIndex(prevWeaponIndex).gameObject.SetActive(false);
  }

  public Gun CurrentWeapon {
    get { return currentWeapon; }
  }
}