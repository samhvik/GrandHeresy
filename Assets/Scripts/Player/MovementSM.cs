using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Walking walkingState;
    [HideInInspector]
    public Running runningState;
    [HideInInspector]
    public Aim aimState;
    [HideInInspector]
    public StrafeAim strafeAimState;

    [Header("Found on Awake")]
    public PlayerControls controls;
    AnimatorManager animatorManager;

    [Header("Found on Start")]
    public Rigidbody rb;

    [Header("Player Movement")]
    private Vector2 left;
    private Vector2 right;
    public float left_horizontal;
    public float left_vertical;
    public float right_horizontal;
    public float right_vertical;
    public Vector3 faceDirection;
    [SerializeField] [Range(50f, 900f)]
    public float lookSpeed = 250f;


    private void Awake(){
        idleState = new Idle(this);
        walkingState = new Walking(this);
        aimState = new Aim(this);
        runningState = new Running(this);
        strafeAimState = new StrafeAim(this);

        initValue();
    }

    protected override BaseState GetInitialState(){
        return idleState;
    }

    void initValue(){
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        animatorManager = GetComponent<AnimatorManager>();

        // callback functions for movement. stores joystick values into "left" Vector2.
        controls.Gameplay.Move.performed += ctx => left = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => left = Vector2.zero;
        
        // callback functions for aiming. stores joystick values into "right" Vector2.
        controls.Gameplay.Aim.performed += ctx => right = ctx.ReadValue<Vector2>();
        controls.Gameplay.Aim.canceled += ctx => right = Vector2.zero;

        // callback function for running.
        // controls.Gameplay.Run.performed += ctx => {ChangeState(runningState);};

        // callback function for dodging.
        // controls.Gameplay.Dodge.performed += ctx => BeginDodge();
    }

    // ------- There must be a way to make getting inputs and animating its own scripts-------------
    void FixedUpdate(){
        GetInput();
        animatorManager.HandleAnimatorValues(left_horizontal, left_vertical, right_horizontal, right_vertical, false);
    }

    private void GetInput(){
        left_horizontal = left.x;
        left_vertical = left.y;

        right_horizontal = right.x;
        right_vertical = right.y;
    }
    // --------------------------------------------------------------------------------

    // Enable and disable control input when script is enabled/disabled.
    public void OnEnable(){
        controls.Gameplay.Enable();
    }

    public void OnDisable(){
        controls.Gameplay.Disable();
    }

}