using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeAim : Moving
{   
    private float L_horizontalInput;
    private float L_verticalInput;
    private float R_horizontalInput;
    private float R_verticalInput;
    private float lastTimeKeyPressDelay;

    public StrafeAim(MovementSM stateMachine) : base("StrafeAim", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;

        // lastTimeKeyPressDelay = sm.lastTimeMovePressed + 1;

        sm.animatorManager.HandleIdleAimState(false);
        sm.animatorManager.HandleKeyboardAimState(true);
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

        // If a Controller is being used, aim with this method
        if (GameValues.instance.whatGamepad[sm.input.playerIndex] == "controller")
        {
            R_horizontalInput = sm.right_horizontal;
            R_verticalInput = sm.right_vertical;

            sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
            var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
            sm.transform.rotation = Quaternion.Lerp(sm.transform.rotation, desiredRotation, 8 * Time.deltaTime);

            // If the right stick is not in motion, switch to idle state
            if (L_horizontalInput == 0 && L_verticalInput == 0)
            {
                sm.animatorManager.HandleKeyboardAimState(false);
                stateMachine.ChangeState(sm.aimState);
            }

            else if (R_horizontalInput == 0 && R_verticalInput == 0){
                sm.animatorManager.HandleKeyboardAimState(false);
                stateMachine.ChangeState(sm.idleState);
            }
        }
        // If a Keyboard is being used, aim with this method  
        else if(GameValues.instance.whatGamepad[sm.input.playerIndex] == "keyboard")
        {
            // R_horizontalInput = sm.right_horizontal;
            // R_verticalInput = sm.right_vertical;

            // sm.animatorManager.HandleAnimatorValues(sm.left_horizontal, sm.left_vertical, sm.right_horizontal, sm.right_vertical, false);

            // sm.faceDirection = GameValues.instance.playerCursors[sm.input.playerIndex].transform.position;
            // var desiredRotation = Quaternion.LookRotation(new Vector3(sm.faceDirection.x, 0, sm.faceDirection.z));
            // sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, 800 * Time.deltaTime);

            Vector3 direction = GameValues.instance.playerCursors[sm.input.playerIndex].transform.position - sm.transform.position;
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            sm.transform.rotation = Quaternion.Lerp(sm.transform.rotation, rotation, 15 * Time.deltaTime);

            //sm.transform.rotation = Quaternion.Euler(0, sm.transform.eulerAngles.y, 0);
            //sm.transform.LookAt(GameValues.instance.playerCursors[sm.input.playerIndex].transform);


            //sm.transform.LookAt(GameValues.instance.playerCursors[sm.input.playerIndex].transform);
            //sm.transform.rotation = Quaternion.Euler(0, sm.transform.eulerAngles.y, 0);

            //sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
            //var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
            //sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, sm.lookSpeed * Time.deltaTime);

            if (L_horizontalInput == 0 && L_verticalInput == 0)
            {
                // if(Time.time > lastTimeKeyPressDelay)
                // {
                //     sm.lastTimeMovePressed = Time.time;
                    sm.animatorManager.HandleKeyboardAimState(false);
                    stateMachine.ChangeState(sm.aimState);
                // }
            }

            else if (!sm.isAiming){
                sm.animatorManager.HandleKeyboardAimState(false);
                stateMachine.ChangeState(sm.idleState);
            }

        }
        
    }
}
