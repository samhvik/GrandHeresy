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

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var playerIndex = playerInput.playerIndex;
        Debug.Log("Player Index: " + playerIndex);
    }
}
