/*
    PlayerInputHandler.cs

    Handles differentiating our players, mainly baed on the player's index. (0-3)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public int playerIndex;

    // Holds the rings that appear under our players
    public GameObject[] playerRings = new GameObject[4];
    public GameObject currentRing;

    // Holds the Colored Cursor for the keyboard user
    public GameObject[] playerCursors = new GameObject[4];
    public GameObject currentCursor;

    private void Awake()
    {
        // Assigns the Player Index
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;

        // Increases how many players are in the game
        GameValues.instance.numPlayers++;

        // Adds Player to Player Array in Game Values
        GameValues.instance.Players[playerIndex] = this.gameObject;

        // Sets player to be alive initially
        GameValues.instance.playerAlive[playerIndex] = true;
        GameValues.instance.numAlive++;

        Debug.Log("Player Index: " + playerIndex + " | Number of Players: " + GameValues.instance.getNumPlayers());

        // Setting the color of our player outlines & instantiating the ring as a child of our player
        setPlayerOutlines();
        currentRing = Instantiate(currentRing, new Vector3(this.transform.position.x, this.transform.position.y - 0.95f, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
        currentRing.transform.parent = this.transform;

        // Setting our cursor
        GameValues.instance.playerCursors[playerIndex] = currentCursor;
    }

    void Update()
    {
        // Gives updates to the players position to GameValues script so that we can track the Camera based on midpoint
        GameValues.instance.playerPosition[playerIndex] = this.GetComponent<Transform>();
    }

    public int getPlayerIndex()
    {
        return playerIndex;
    }

    // Sets up if the player is using a controller or a keyboard and mouse.
    public void SetGamepad(InputAction.CallbackContext context)
    {
        bool isKeyboard = context.control.device is Keyboard;
        bool isGamepad = context.control.device is Gamepad;
        
        if(isKeyboard)
        {
            GameValues.instance.whatGamepad[playerIndex] = "keyboard";
        }
        else if(isGamepad)
        {
            GameValues.instance.whatGamepad[playerIndex] = "controller";
        }
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
