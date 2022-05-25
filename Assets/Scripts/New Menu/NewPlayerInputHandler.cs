/*
    PlayerInputHandler.cs

    Handles differentiating our players, mainly baed on the player's index. (0-3)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class NewPlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public int playerIndex;

    private PlayerConfiguration playerConfig;
    private PlayerControls controls;

    // Holds the rings that appear under our players
    public GameObject[] playerRings = new GameObject[4];
    public GameObject currentRing;

    // Holds the Colored Cursor for the keyboard user
    public GameObject[] playerCursors = new GameObject[4];
    public GameObject currentCursor;

    // Holds the Player Spawning Particles
    public ParticleSystem[] playerSpawn = new ParticleSystem[7];

    private bool gamepadLock;

    private void Awake()
    {
        // Assigns the Player Index
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;

        // Init player controls
        controls = new PlayerControls();

        // Increases how many players are in the game
        // GameValues.instance.numPlayers++;

        // Adds Player to Player Array in Game Values
        // GameValues.instance.players[playerIndex] = this.gameObject;

        // Sets player to be alive initially
        // GameValues.instance.playerAlive[playerIndex] = true;
        // GameValues.instance.numAlive++;

        // Debug.Log("Player Index: " + playerIndex + " | Number of Players: " + GameValues.instance.getNumPlayers());

        // Setting the color of our player outlines & instantiating the ring as a child of our player
        

        

        

        // if (GameValues.instance.whatGamepad[playerIndex] == "keyboard")
        // {
        //     // if (GameValues.instance.cursorLock == false)
        //     // {
                
        //     // }
        // }

        // SetGamepad(playerInput);
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        // playerConfig.transform.parent = 

        setPlayerOutlines();
        currentRing = Instantiate(currentRing, new Vector3(this.transform.position.x, this.transform.position.y - 0.95f, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
        currentRing.transform.parent = this.transform;

        // Playing our Partilce System
        for(int i = 0; i < 7; i++)
        {
            Instantiate(playerSpawn[i], new Vector3(this.transform.position.x, this.transform.position.y - 0.95f, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
        }

        // Setting our cursor
        GameValues.instance.playerCursors[playerIndex] = currentCursor;
        gamepadLock = false;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
    //     if(obj.action)
    }

    void Update()
    {
        // Gives updates to the players position to GameValues script so that we can track the Camera based on midpoint
        GameValues.instance.playerPosition[playerIndex] = this.GetComponent<Transform>();

        // // Instantiates Cursor
        // if (GameValues.instance.whatGamepad[playerIndex] != "controller" && gamepadLock == true)
        // {
        //     if (GameValues.instance.cursorLock == false)
        //     {
        //         GameValues.instance.playerCursors[playerIndex] = Transform.Instantiate(GameValues.instance.playerCursors[playerIndex], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
        //         GameValues.instance.cursorLock = true;
        //     }
        // }
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    // Sets up if the player is using a controller or a keyboard and mouse.
    public void SetGamepad(InputAction.CallbackContext context)
    {

        if(GameValues.instance.whatGamepad[playerIndex] == null)
        {
            bool isMouse = context.control.device is Mouse;
            bool isKeyboard = context.control.device is Keyboard;
            
            bool isGamepad = context.control.device is Gamepad;
            
            if(isMouse || isKeyboard)
            {
                GameValues.instance.whatGamepad[playerIndex] = "keyboard";
                gamepadLock = true;

                GameValues.instance.playerCursors[playerIndex] = Transform.Instantiate(GameValues.instance.playerCursors[playerIndex], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
                GameValues.instance.cursorLock = true;
            }
            else if(isGamepad)
            {
                GameValues.instance.whatGamepad[playerIndex] = "controller";
                gamepadLock = true;
            }
        }
    }

    public void SetKeyboardAsGamepad(InputAction.CallbackContext context){

        // Debug.Log("Setting Keyboard as Gamepad...");
        // Debug.Log(GameValues.instance.whatGamepad[playerIndex]);
        if(GameValues.instance.whatGamepad[playerIndex] == "")
        {
            Debug.Log("Setting Keyboard as Gamepad...");
            GameValues.instance.whatGamepad[playerIndex] = "keyboard";
            gamepadLock = true;

            GameValues.instance.playerCursors[playerIndex] = Transform.Instantiate(GameValues.instance.playerCursors[playerIndex], new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
            GameValues.instance.cursorLock = true;
        }

    }

    public void SetControllerAsGamepad(InputAction.CallbackContext context){
        GameValues.instance.whatGamepad[playerIndex] = "controller";
        gamepadLock = true;
    }

    // Function will set the players outlines to specific colors & assign which color ring to place under player & Cursor Colors
    public void setPlayerOutlines()
    {

        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 6f;

        if (playerIndex == 0)
        {
            outline.OutlineColor = Color.red;
            currentRing = playerRings[0];
            currentCursor = playerCursors[0];
        }
        else if(playerIndex == 1)
        {
            outline.OutlineColor = Color.green;
            currentRing = playerRings[1];
            currentCursor = playerCursors[1];
        }
        else if (playerIndex == 2)
        {
            outline.OutlineColor = Color.blue;
            currentRing = playerRings[2];
            currentCursor = playerCursors[2];
        }
        else if (playerIndex == 3)
        {
            outline.OutlineColor = Color.yellow;
            currentRing = playerRings[3];
            currentCursor = playerCursors[3];
        }
    }
}
