using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//                      * IN PROGRESS *
public static class Algorithms
{
    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
//

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
            //Debug.Log("HORIZONTAL: " + R_horizontalInput);
            //Debug.Log("VERTICAL: " + R_verticalInput);

            sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
            var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
            sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, sm.lookSpeed * Time.deltaTime);

            // If the right stick is not in motion, switch to idle state
            if (R_horizontalInput == 0 && R_verticalInput == 0)
            {
                stateMachine.ChangeState(sm.idleState);
            }
        } 
        // If a Keyboard is being used, aim with this method                            * IN PROGRESS *
        else
        {
            R_horizontalInput = sm.right_horizontal;
            //R_horizontalInput = Algorithms.Remap(R_horizontalInput,-1200, 1200, -1, 1);
            //Debug.Log("HORIZONTAL: " + R_horizontalInput);

            R_verticalInput = sm.right_vertical;
            //R_verticalInput = Algorithms.Remap(R_verticalInput, -600, 600, -1, 1);
            //Debug.Log("VERTICAL: " + R_verticalInput);

            //Vector3 temp = new Vector3(R_horizontalInput, 0, R_verticalInput);
            //sm.transform.LookAt(temp);

            //sm.faceDirection = Vector3.forward * R_verticalInput + Vector3.right * R_horizontalInput;
            //var desiredRotation = Quaternion.LookRotation(sm.faceDirection);
            // sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, sm.lookSpeed * Time.deltaTime);

            // If the right stick is not in motion, switch to idle state
            if (R_horizontalInput == 0 && R_verticalInput == 0)
            {
                stateMachine.ChangeState(sm.idleState);
            }
        }
    }

}
