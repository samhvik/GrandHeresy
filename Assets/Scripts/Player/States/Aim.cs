using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        // If a Controller is being used, aim with this method
        if (GameValues.instance.whatGamepad[sm.input.playerIndex] == "controller")
        {
            R_horizontalInput = sm.right_horizontal;
            R_verticalInput = sm.right_vertical;

            sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
            var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
            sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, sm.lookSpeed * Time.deltaTime);

            // If the right stick is not in motion, switch to idle state
            if (R_horizontalInput == 0 && R_verticalInput == 0)
            {
                stateMachine.ChangeState(sm.idleState);
            }
        } 
        // If a Keyboard is being used, aim with this method                         
        else
        {
            if (GameValues.instance.cursorLock == false)
            {
                GameValues.instance.playerCursors[sm.input.playerIndex] = Transform.Instantiate(GameValues.instance.playerCursors[sm.input.playerIndex], new Vector3(sm.transform.position.x, sm.transform.position.y, sm.transform.position.z), sm.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
                GameValues.instance.cursorLock = true;
            }

            sm.transform.LookAt(GameValues.instance.playerCursors[sm.input.playerIndex].transform);
            sm.transform.rotation = Quaternion.Euler(0, sm.transform.eulerAngles.y, 0);

            if (sm.isAiming == false)
            {
                stateMachine.ChangeState(sm.idleState);
            }
        }
    }

}
