/*
    PlayerMovements.cs

    Controls the movement of the Player object.
    Uses new InputSystem with Callback Context
*/
#pragma warning disable 108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    public enum PlayerMovementState{
        Idle,
        Walking,
        Running,
        Aiming
    }

    public PlayerMovementState currentState = PlayerMovementState.Idle;

    // Character Controller GameObject
    private CharacterController controller;

    // Handling Movement
    private Vector2 movementInput = Vector2.zero;
    private float left_horizontal;
    private float left_vertical;

    // Handling Aiming
    [SerializeField]
    [Range(50f, 500f)]
    private float lookSpeed = 250f;
    private Vector2 aimInput = Vector2.zero;
    private Vector3 faceDirection;
    private float right_horizontal;
    private float right_vertical;

    private Transform transform;

    void Awake()
    {
        // Grabbing our character controller that is on our player
        controller = gameObject.GetComponent<CharacterController>();

        transform = this.GetComponent<Transform>();
    }

    // OnMove moves our player with the left joystick
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // OnAim rotates our player to aim with the right joystick
    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    // OnRun toggles our players sprinting
    public void OnRun()
    {
        currentState = PlayerMovementState.Running;
    }

    void Update(){
        GetInput();
        
        switch(currentState){
            case PlayerMovementState.Idle:
                break;
            case PlayerMovementState.Walking:
                Walk();
                break;
            case PlayerMovementState.Running:
                Run();
                break;
            case PlayerMovementState.Aiming:
                AimWalk();
                Aim();
                break;
            default:
                Debug.Log("Invalid PlayerMovementState Detected");
                break;
        }
    }

    private void GetInput(){

        // Set Left Joystick Values
        left_horizontal = movementInput.x;
        left_vertical = movementInput.y;

        // Set Right Joystick Values
        right_horizontal = aimInput.x;
        right_vertical = aimInput.y;

        // AIMING
        if(Mathf.Abs(right_horizontal) > 0.1f || Mathf.Abs(right_vertical) > 0.1f){
            currentState = PlayerMovementState.Aiming;
            return;
        }
        
        // BEGIN RUNNING
        if(Input.GetButtonDown("Run") && currentState == PlayerMovementState.Walking){
            currentState = PlayerMovementState.Running;
            return;
        }

        // END RUNNING
        if(left_horizontal == 0f && left_vertical == 0f && currentState == PlayerMovementState.Running){
            currentState = PlayerMovementState.Idle;
            return;
        }

        // WHILE RUNNING
        if(currentState == PlayerMovementState.Running){
            return;
        }
        
        // WALKING
        if(Mathf.Abs(left_horizontal) > 0.1f || Mathf.Abs(left_vertical) > 0.1f){
            currentState = PlayerMovementState.Walking;
            return;
        }

        // IDLE
        currentState = PlayerMovementState.Idle;
    }

    private void Walk(){
        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            left_horizontal * GameValues.instance.playerSpeedWalk,
            0.0f,
            left_vertical * GameValues.instance.playerSpeedWalk
        ));

        // Bug: For some reason, face direction is different here than in Aim()
        faceDirection = Vector3.forward * left_horizontal + Vector3.left * left_vertical;
        if(faceDirection.sqrMagnitude > 0.2f)
            transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    private void AimWalk(){
        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            left_horizontal * GameValues.instance.playerSpeedAim,
            0.0f,
            left_vertical * GameValues.instance.playerSpeedAim
        ));
    }

    private void Run(){
        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            left_horizontal * GameValues.instance.playerSpeedRun,
            0.0f,
            left_vertical * GameValues.instance.playerSpeedRun
        ));

        // Bug: For some reason, face direction is different here than in Aim()
        faceDirection = Vector3.forward * left_horizontal + Vector3.left * left_vertical;
        if(faceDirection.sqrMagnitude > 0.2f)
            transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    private void Aim(){
        faceDirection = Vector3.forward * right_horizontal + Vector3.left * right_vertical;
        var desiredRotation = Quaternion.LookRotation(faceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, lookSpeed * Time.deltaTime);
    }
}
