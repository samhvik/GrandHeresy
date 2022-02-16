using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeAim : Moving
{   
    private float L_horizontalInput;
    private float L_verticalInput;
    private float R_horizontalInput;
    private float R_verticalInput;

    public StrafeAim(MovementSM stateMachine) : base("StrafeAim", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;

        sm.GetComponent<CharacterController>().SimpleMove(new Vector3(
            L_horizontalInput * GameValues.instance.playerSpeedAim,
            0.0f,
            L_verticalInput * GameValues.instance.playerSpeedAim
        ));

        sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
        var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
        sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, sm.lookSpeed * Time.deltaTime);

        // If the right stick is not in motion, switch to idle state
        if(R_horizontalInput == 0 && R_verticalInput == 0){
            stateMachine.ChangeState(sm.idleState);
        }
        
    }
}
