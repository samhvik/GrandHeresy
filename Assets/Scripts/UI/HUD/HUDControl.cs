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
    public Sprite[] weaponSprites = new Sprite[7];
    public Image[] weaponIcons = new Image[4];
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

    }

    // Update is called once per frame
    void Update()
    {

        if(GameValues.instance.isPaused)
        {
            deActivateHUDElemnt(GameValues.instance.getNumPlayers());
        }

        else
        {
            activateHUDElemnt(GameValues.instance.getNumPlayers());
            if(GameValues.instance.objectivesCompleted< GameValues.instance.objectivesTotal){
                objectiveText.text = "Objectives Completed: " + GameValues.instance.objectivesCompleted;
            }
            else{
                objectiveText.text = "Proceed to extraction";
            }
        }
        
    }
    void LateUpdate()
    {
        
        // Enabling Text to Display and Update Text
        for (int i = 0; i < GameValues.instance.getNumPlayers(); i++)
        {
            PlayerInventory inventory = GameValues.instance.getPlayer(i).GetComponentInChildren<PlayerInventory>(); // Danny: Change when implementing to main
            //print(inventory.CurrentWeapon.Name);
            switch(inventory.CurrentWeapon.Name){
                case "Apostle":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(30,20);
                    weaponIcons[i].sprite = weaponSprites[0];
                    break;
                case "Aribiter":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(68,20);
                    weaponIcons[i].sprite = weaponSprites[1];
                    break;
                case "Bishop":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(68,20);
                    weaponIcons[i].sprite = weaponSprites[2];
                    break;
                case "Harpy":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(68,20);
                    weaponIcons[i].sprite = weaponSprites[3];
                    break;
                case "Patriarch":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(80,20);
                    weaponIcons[i].sprite = weaponSprites[4];
                    break;
                case "Pilate":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(80,20);
                    weaponIcons[i].sprite = weaponSprites[5];
                    break;
                case "Vindicator":
                    weaponIcons[i].rectTransform.sizeDelta = new Vector2(75,20);
                    weaponIcons[i].sprite = weaponSprites[6];
                    break;
                default:
                    break;
            }
            // weaponIcons[i].sprite = 
            
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
    //Unfinished
    void changeWeapon(){

    }

    void activateHUDElemnt(int numPlayers){
        for(int i = 0; i < numPlayers; i++){
            playerHUDPanels[i].SetActive(true);
            playerHUDHighlights[i].SetActive(true);
        }
       
    }

    void deActivateHUDElemnt(int numPlayers){
        for(int i = 0; i < numPlayers; i++){
            playerHUDPanels[i].SetActive(false);
            playerHUDHighlights[i].SetActive(false);
        }
       
    }
    
}
