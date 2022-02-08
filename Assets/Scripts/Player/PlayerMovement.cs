/*
    PlayerMovement.cs

    Controls the movement of the Player object.
    References the GameValues instance for movement values.
*/
#pragma warning disable 108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerMovementState{
        Idle,
        Walking,
        Running,
        Aiming,
        Dodge
    }

    public PlayerMovementState currentState = PlayerMovementState.Idle;

    [Header("Found on Awake")]
    PlayerControls controls;
    AnimatorManager animatorManager;
    public StateProcessor stateProcessor;

    [Header("Found on Start")]
    private Transform transform;
    private Rigidbody rb;
    private Vector3 position;


    [Header("Player Movement")]
    private Vector2 left;
    private Vector2 right;
    private float left_horizontal;
    private float left_vertical;
    private float right_horizontal;
    private float right_vertical;
    private Vector3 faceDirection;
    [SerializeField] [Range(50f, 900f)]
    private float lookSpeed = 250f;


    void Awake(){
        controls = new PlayerControls();
        animatorManager = GetComponent<AnimatorManager>();

        // init state machine
        // stateProcessor = this.gameObject.GetComponentInChildren<StateProcessor>();
        // stateProcessor.TransitionTo(typeof(Idle));

        // callback functions for movement. stores joystick values into "left" Vector2.
        controls.Gameplay.Move.performed += ctx => left = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => left = Vector2.zero;
        
        // callback functions for aiming. stores joystick values into "right" Vector2.
        controls.Gameplay.Aim.performed += ctx => right = ctx.ReadValue<Vector2>();
        controls.Gameplay.Aim.canceled += ctx => right = Vector2.zero;

        // callback function for running.
        controls.Gameplay.Run.performed += ctx => BeginRun();

        // callback function for dodging.
        controls.Gameplay.Dodge.performed += ctx => BeginDodge();
        
    }

    // Enable and disable control input when script is enabled/disabled.
    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }

    void Start(){
        transform = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        position = transform.position;

        // cam = Camera.main.transform;

        left_horizontal = 0.0f;
        left_vertical = 0.0f;

        right_horizontal = 0.0f;
        right_vertical = 0.0f;
    }

    
    void Update(){
        GetInput();
        animatorManager.HandleAnimatorValues(left_horizontal, left_vertical, right_horizontal, right_vertical, false);
        //Movement();

        // Camera math to animate character the right way when aiming
        // if(cam != null){
        //     camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized; 
        //     move = left_vertical * camForward + left_horizontal * cam.right;
        // }
        // else{
        //     move = left_vertical * Vector3.forward + left_horizontal * Vector3.right;
        // }

        // if(move.magnitude > 1){
        //     move.Normalize();
        // }

        // Move(move);
        
        
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
            // case PlayerMovementState.Dodge:
            //     break;
            default:
                Debug.Log("Invalid PlayerMovementState Detected");
                break;
        }
    }
    // private void Move(Vector3 move){
    //     if(move.magnitude > 1){
    //         move.Normalize();
    //     }
    //     this.moveInput = move;
    //     ConvertMoveInput();
    // }
    // private void ConvertMoveInput(){
    //     Vector3 localMove = transform.InverseTransformDirection(moveInput);
    //     turnAmount = localMove.x;
    //     forwardAmount = localMove.z;
    //     animatorManager.HandleAnimatorValues(turnAmount, forwardAmount, 0, 0, false);
    // }

    private void GetInput(){
        left_horizontal = left.x;
        left_vertical = left.y;

        right_horizontal = right.x;
        right_vertical = right.y;

        // AIMING
        if(Mathf.Abs(right_horizontal) > 0.1f || Mathf.Abs(right_vertical) > 0.1f){
            currentState = PlayerMovementState.Aiming;
            return;
        }
        
        // // BEGIN RUNNING
        if(Input.GetButtonDown("Run") && currentState == PlayerMovementState.Walking){
            currentState = PlayerMovementState.Running;
            return;
        }

        // BEGIN DODGING
        // if(Input.GetButtonDown("Dodge") && currentState == PlayerMovementState.Walking){
        //     currentState = PlayerMovementState.Dodge;
        //     return;
        // }

        // // END RUNNING
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
        // currentState = PlayerMovementState.Idle;
    }

    private void Walk(){
        animatorManager.HandleAnimatorValues(left_horizontal, left_vertical, 0.0f, 0.0f, false);

        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            left_horizontal * GameValues.instance.playerSpeedWalk,
            0.0f,
            left_vertical * GameValues.instance.playerSpeedWalk
        ));

        // Bug: For some reason, face direction is different here than in Aim()
        // Bug potentially fixed. Put left_vertical with the forward vector and left_horizontal with 
        // Vector3.right instead of left.
        // faceDirection = Vector3.forward * left_vertical + Vector3.right * left_horizontal;

        // Debug.DrawLine(this.transform.position, faceDirection, Color.red);
        // if(faceDirection.sqrMagnitude > 0.1f){
        //     this.transform.rotation = Quaternion.LookRotation(faceDirection);
        // }

        faceDirection = new Vector3(left_horizontal, 0 , left_vertical);
        faceDirection.Normalize();

        transform.Translate(faceDirection * Time.deltaTime, Space.World);

        if(faceDirection != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(faceDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, lookSpeed * Time.deltaTime);
        }

    }

    private void AimWalk(){
        animatorManager.HandleAnimatorValues(left_horizontal, left_vertical, right_horizontal, right_vertical, false);

        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            left_horizontal * GameValues.instance.playerSpeedAim,
            0.0f,
            left_vertical * GameValues.instance.playerSpeedAim
        ));
    }

    private void BeginRun(){
        currentState = PlayerMovementState.Running;
    }

    private void BeginDodge(){
        currentState = PlayerMovementState.Dodge;
    }

    private void Run(){

        animatorManager.HandleAnimatorValues(left_horizontal, left_vertical, 0.0f, 0.0f, true);

        this.GetComponent<CharacterController>().SimpleMove(new Vector3(
            left_horizontal * GameValues.instance.playerSpeedRun,
            0.0f,
            left_vertical * GameValues.instance.playerSpeedRun
        ));

        faceDirection = Vector3.forward * left_vertical + Vector3.right * left_horizontal;
        if(faceDirection.sqrMagnitude > 0.2f)
            transform.rotation = Quaternion.LookRotation(faceDirection);
    }

    private void Dodge(){

    }

    private void Aim(){
        faceDirection = Vector3.forward * right_vertical + Vector3.right * right_horizontal;
        var desiredRotation = Quaternion.LookRotation(faceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, lookSpeed * Time.deltaTime);
    }
}
