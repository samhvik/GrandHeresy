using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    public float verticalAimInput;
    public float horizontalAimInput;
    private Vector2 movementInput;
    private Vector2 aimInput;

    [SerializeField] [Range(50f, 500f)]
    private float lookSpeed = 250f;

    [Header("Button Inputs")]
    public bool runInput;

    private void Awake(){
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnEnable(){
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            playerControls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;

            // callback functions for aiming. stores joystick values into "right" Vector2.
            playerControls.Gameplay.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
            playerControls.Gameplay.Aim.canceled += ctx => aimInput = Vector2.zero;

            playerControls.Gameplay.Run.performed += ctx => runInput = true;
            playerControls.Gameplay.Run.canceled += ctx => runInput = false;
        }

        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public void HandleAllInputs(){
        HandleMovementInput();
        HandleAimWalk();
        HandleAim();
    }

    private void HandleMovementInput(){
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        //animatorManager.HandleAnimatorValues(horizontalMovementInput, verticalMovementInput, runInput);

        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            horizontalMovementInput * GameValues.instance.playerSpeedWalk,
            0.0f,
            verticalMovementInput * GameValues.instance.playerSpeedWalk
        ));

        // Bug: For some reason, face direction is different here than in Aim()
        var faceDirection = Vector3.forward * horizontalMovementInput + Vector3.left * verticalMovementInput;
        if(faceDirection.sqrMagnitude > 0.2f)
            transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    private void HandleAim(){
        horizontalAimInput = aimInput.x;
        verticalAimInput = aimInput.y;

        var faceDirection = Vector3.forward * horizontalAimInput + Vector3.left * verticalAimInput;
        var desiredRotation = Quaternion.LookRotation(faceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, lookSpeed * Time.deltaTime);
    }

    private void HandleAimWalk(){
        // this.GetComponent<CharacterController>().SimpleMove(new Vector3(
        //     left_horizontal * GameValues.instance.playerSpeedAim,
        //     0.0f,
        //     left_vertical * GameValues.instance.playerSpeedAim
        // ));
    }
}
