using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDControl : MonoBehaviour
{
    public Text objectiveText;
    public Text[] ammoText = new Text[4];
    public Text[] healthText = new Text[4];
    public GameObject[] playerHUDPanels = new GameObject[4];
    public GameObject[] playerHUDHighlights = new GameObject[4];
    // Start is called before the first frame update
    void Awake()
    {
       playerHUDPanels[1].SetActive(false);
       playerHUDPanels[2].SetActive(false);
       playerHUDPanels[3].SetActive(false);
       playerHUDHighlights[1].SetActive(false);
       playerHUDHighlights[2].SetActive(false);
       playerHUDHighlights[3].SetActive(false);
    }
    void Start()
    {
       print("Number of Players" );
       print(GameValues.instance.getNumPlayers());
    }

    // Update is called once per frame
    void Update()
    {
        activateHUDElemnt(GameValues.instance.getNumPlayers());
        if(GameValues.instance.objectivesCompleted< GameValues.instance.objectivesTotal){
            objectiveText.text = "Objectives Completed: " + GameValues.instance.objectivesCompleted;
        }
        else{
            objectiveText.text = "Proceed to extraction";
        }
    }
    void LateUpdate()
    {
        
        // Enabling Text to Display and Update Text
        for (int i = 0; i < GameValues.instance.getNumPlayers(); i++)
        {
            PlayerInventory inventory = GameValues.instance.getPlayer(i).GetComponentInChildren<PlayerInventory>(); // Danny: Change when implementing to main
            //set health text
            healthText[i].text = inventory.health + "";
            // Set the ammo text
            
            switch (inventory.CurrentWeapon.shootState) // Danny: Change when implementing to main
            {
                case Gun.ShootState.Reloading:
                    ammoText[i].text = "R";
                    break;
                default:
                    ammoText[i].text = inventory.CurrentWeapon.CurrentAmmo + "";
                    break;
            }
        }
    }
    void changeWeapon(){

    }
    void activateHUDElemnt(int numPlayers){
        for(int i = 0; i < numPlayers; i++){
            playerHUDPanels[i].SetActive(true);
            playerHUDHighlights[i].SetActive(true);
        }
       
    }
    
}
