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
    int playerIndex;

    private void Awake()
    {
        // Assigns the Player Index
        playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex;

        // Increases how many players are in the game
        GameValues.instance.numPlayers++;

        Debug.Log("Player Index: " + playerIndex + " | Number of Players: " + GameValues.instance.numPlayers);
    }

    void Update()
    {
        // Gives updates to the players position to GameValues script so that we can track the Camera based on midpoint
        GameValues.instance.playerPosition[playerIndex] = this.GetComponent<Transform>();
    }
}
