using UnityEngine;

public class Idle : Stationary
{
    private float R_horizontalInput;
    private float R_verticalInput;
    
    public Idle (MovementSM stateMachine) : base("Idle", stateMachine) { }

    public override void Enter(){
        base.Enter();
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;
    }

    public override void UpdateLogic(){
        base.UpdateLogic();
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;

        // If the right stick is in motion, switch to aim state
        if (R_horizontalInput != 0 || R_verticalInput != 0 || sm.isAiming == true)
        {
            stateMachine.ChangeState(sm.aimState);
        }
    }

}