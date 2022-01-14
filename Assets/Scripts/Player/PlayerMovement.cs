/*
    PlayerMovement.cs

    Controls the movement of the Player object.
    References the GameValues instance for movement values.
*/
#pragma warning disable 108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerMovementState{
        Idle,
        Walking,
        Running,
        Aiming
    }

    public PlayerMovementState currentState = PlayerMovementState.Idle;

    private Transform transform;
    private Rigidbody rb;
    private Vector3 position;

    private float left_horizontal;
    private float left_vertical;

    private float right_horizontal;
    private float right_vertical;
    private Vector3 faceDirection;

    [SerializeField] [Range(50f, 500f)]
    private float lookSpeed = 250f;

    
    void Start(){
        transform = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
        position = transform.position;

        left_horizontal = 0.0f;
        left_vertical = 0.0f;

        right_horizontal = 0.0f;
        right_vertical = 0.0f;
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
        left_horizontal = Input.GetAxis("Horizontal");
        left_vertical = Input.GetAxis("Vertical");

        right_horizontal = Input.GetAxis("R_Horizontal");
        right_vertical = Input.GetAxis("R_Vertical");

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
        faceDirection = Vector3.right * right_horizontal + Vector3.forward * right_vertical;
        var desiredRotation = Quaternion.LookRotation(faceDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, lookSpeed * Time.deltaTime);
    }
}
