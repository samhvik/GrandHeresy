using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : Moving
{
    private float L_horizontalInput;
    private float L_verticalInput;
    private float dodgeDirection;

    public Dodge (MovementSM stateMachine) : base("Dodge", stateMachine) { }

    public override void Enter(){
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
    }

    public override void UpdateLogic(){
        base.UpdateLogic();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        Debug.DrawLine(sm.transform.position, new Vector3(L_horizontalInput, 0, L_verticalInput), Color.green);

        // If the left stick is in motion, switch to walking state
        if(L_horizontalInput == 0 && L_verticalInput == 0){
            stateMachine.ChangeState(sm.idleState);
        }
    }

}
