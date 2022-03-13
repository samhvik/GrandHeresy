using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Walking walkingState;
    [HideInInspector]
    public Running runningState;
    [HideInInspector]
    public Dodge dodgeState;
    [HideInInspector]
    public Aim aimState;
    [HideInInspector]
    public StrafeAim strafeAimState;

    [Header("Found on Awake")]
    public PlayerControls controls;
    public AnimatorManager animatorManager;
    public Rig rigLayer_HandIK;
    public PlayerInputHandler input;

    [Header("Player Movement")]
    public float left_horizontal;
    public float left_vertical;
    public float right_horizontal;
    public float right_vertical;
    public Vector3 faceDirection;
    private Vector2 left;
    private Vector2 right;
    private Vector2 movementInput = Vector2.zero;
    private Vector2 aimInput = Vector2.zero;
    public bool isAiming = false;

    [Header("Player Tweaking")]
    [SerializeField] [Range(200f, 900f)]
    public float lookSpeed = 250f;
    [SerializeField] [Range(50f, 300f)]
    public float dodgeSlideSpeed = 250f;
    [SerializeField] [Range(1f, 20f)]
    public float dodgeSlideFalloff = 10f;


    private void Awake(){
        idleState = new Idle(this);
        walkingState = new Walking(this);
        aimState = new Aim(this);
        runningState = new Running(this);
        strafeAimState = new StrafeAim(this);
        dodgeState = new Dodge(this);

        input = this.gameObject.GetComponent<PlayerInputHandler>();

        initValue();
    }

    // OnMove moves our player with the left joystick
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // OnAim rotates our player to aim with the right joystick
    public void OnAim(InputAction.CallbackContext context)
    {
        if (GameValues.instance.whatGamepad[this.input.playerIndex] == "controller")
        {
            aimInput = context.ReadValue<Vector2>();
        }
        else
        {
            // Aiming if RMB is held down
            if (context.ReadValue<float>() > 0.1f)
            {
                isAiming = true;
            }
            else
            {
                isAiming = false;
            }
        }
        aimInput = context.ReadValue<Vector2>();
    }

    // OnRun will make our player run
    public void OnRun(InputAction.CallbackContext context)
    {
        ChangeState(runningState);
    }

    // OnDodge will make our player Dodge
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.performed){
            Debug.Log("Dodging...");
            if(GetCurrentState() != "Dodge"){
                ChangeState(dodgeState);
            }
        }
    }

    protected override BaseState GetInitialState(){
        return idleState;
    }

    void initValue(){
        controls = new PlayerControls();
        animatorManager = GetComponent<AnimatorManager>();
    }

    // ------- There must be a way to make getting inputs and animating its own scripts-------------
    void FixedUpdate(){
        GetInput();
        animatorManager.HandleAnimatorValues(left_horizontal, left_vertical, right_horizontal, right_vertical, false);
        // if(controls.Gameplay.Dodge.triggered) {
        //     Debug.Log("DodgeRolling Anim....");
        //     animatorManager.HandleDodgeRollState(true);
        // }
    }

    private void GetInput(){
        // Set Left Joystick Values
        left_horizontal = movementInput.x;
        left_vertical = movementInput.y;

        // Set Right Joystick Values
        right_horizontal = aimInput.x;
        right_vertical = aimInput.y;
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