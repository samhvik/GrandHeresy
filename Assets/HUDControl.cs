using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDControl : MonoBehaviour
{
    public GameObject player1HUDPanel;
    public GameObject player2HUDPanel;
    public GameObject player3HUDPanel;
    public GameObject player4HUDPanel;
    public GameObject player1HUDHighlight;
    public GameObject player2HUDHighlight;
    public GameObject player3HUDHighlight;
    public GameObject player4HUDHighlight;
    public Text player1AmmoText;
    public Text player2AmmoText;
    public Text player3AmmoText;
    public Text player4AmmoText;
    public Text player1HealthText;
    public Text player2HealthText;
    public Text player3HealthText;
    public Text player4HealthText;
    
    private GameObject[] HUDPanels= {player1HUDPanel, player2HUDPanel, player3HUDPanel, player4HUDPanel};
    private GameObject[] HUDHighlights= {player1HUDHighlight, player2HUDHighlight, player3HUDHighlight, player4HUDHighlight};
    // Start is called before the first frame update
    void Awake()
    {
        player2HUDPanel.SetActive(false);
        player2HUDHighlight.SetActive(false);
        player3HUDPanel.SetActive(false);
        player3HUDHighlight.SetActive(false);
        player4HUDPanel.SetActive(false);
        player4HUDHighlight.SetActive(false);
    }
    void Start()
    {
       print("Number of Players" );
       print(GameValues.instance.getNumPlayers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        // Enabling Text to Display and Update Text
        for (int i = 0; i < GameValues.instance.getNumPlayers(); i++)
        {
            PlayerInventory inventory = GameValues.instance.getPlayer(i).GetComponentInChildren<PlayerInventory>(); // Danny: Change when implementing to main
            //set health text

            // Set the weapon text
            // weaponText[i].gameObject.SetActive(true);
            // weaponText[i].text = "" + inventory.CurrentWeapon.Name; // Danny: Change when implementing to main

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
    void updateHealth()
    {

    }
    void updateAmmo()
    {
        // ammoText[i].gameObject
    }
    void changeWeapon(){

    }
    void activateHUDElemnt(int x){
        HUDPanels[x].SetActive(true);
        HUDHighlights[x].SetActive(true);
    }
    
}
