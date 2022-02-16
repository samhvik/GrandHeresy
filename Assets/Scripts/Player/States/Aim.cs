using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : Stationary
{   
    private float R_horizontalInput;
    private float R_verticalInput;

    public Aim(MovementSM stateMachine) : base("Aim", stateMachine) {
        sm = (MovementSM) this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;

        sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
        var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
        sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, sm.lookSpeed * Time.deltaTime);

        // If the right stick is not in motion, switch to idle state
        if(R_horizontalInput == 0 && R_verticalInput == 0){
            stateMachine.ChangeState(sm.idleState);
        }
    }
}
