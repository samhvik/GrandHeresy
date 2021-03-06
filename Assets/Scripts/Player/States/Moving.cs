using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : BaseState
{
    protected MovementSM sm;
    private float L_horizontalInput;
    private float L_verticalInput;

    public Moving (string name, MovementSM stateMachine) : base(name, stateMachine) {
        sm = (MovementSM) this.stateMachine;
    }

    public override void Enter(){
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
    }

    public override void UpdateLogic(){
        base.UpdateLogic();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;

        // If the left stick is not in motion no more, switch to idle state
        if((stateMachine.GetCurrentState() != "Dodge") && (L_horizontalInput == 0 && L_verticalInput == 0)){
            //Debug.Log("Going back to idle...");
            stateMachine.ChangeState(sm.idleState);
        }
    }
}
