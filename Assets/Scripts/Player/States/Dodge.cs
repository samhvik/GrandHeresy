using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : BaseState
{
    private MovementSM sm;
    private float L_horizontalInput;
    private float L_verticalInput;
    private float dodgeDirection;

    private Vector3 moveDir;
    //private float _slideSpeed;
    //private float _slideFalloff;

    private bool isDodging;
    private float dodgeTimer;

    private float startTime;
    private float endTime;

    //private float dodgeCoolDown = 1;
    //private float actCoolDown;


    public Dodge (MovementSM stateMachine) : base("Dodge", stateMachine) {
        sm = (MovementSM) this.stateMachine;
    }

    public override void Enter(){
        base.Enter();
        sm.animatorManager.HandleDodgeRollState(true);
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;

        // Normalize the inputs

        // Starting variables to set
        endTime = Time.fixedTime + sm.dashTime;
        moveDir = new Vector3(L_horizontalInput, 0, L_verticalInput).normalized;
        sm.animatorManager.HandleDodgeRollState(true);

        // Rotation of character
        Quaternion desiredRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, 800f);
    }

    public override void UpdateLogic(){
        base.UpdateLogic();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        
        Debug.DrawLine(sm.transform.position, new Vector3(L_horizontalInput, 0, L_verticalInput), Color.green);

        // Movement of dodge
        sm.characterController.SimpleMove(moveDir * sm.dashSpeed);

        // Finishing the dodge
        if(Time.fixedTime > (endTime - sm.endDashTime))
        {
            //Debug.Log("Slowing down");
            sm.characterController.SimpleMove(-moveDir * sm.endDashSpeed);
        }

        // Finishing the dodge
        if(Time.fixedTime > endTime)
        {
            //Debug.Log("Finished Dodge");
            sm.animatorManager.HandleDodgeRollState(false);
            stateMachine.ChangeState(sm.runningState);
        }
            
    }

    // private bool TryMove(Vector3 baseMoveDir, float distance){
    //     Vector3 _moveDir = baseMoveDir;
    //     bool canMove = CanMove(_moveDir, distance);
    //     if(!canMove){
    //         _moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
    //         canMove = _moveDir.x != 0f && CanMove(_moveDir, distance);
    //         if(!canMove){
    //             _moveDir = new Vector3(0f, baseMoveDir.z).normalized;
    //             canMove = _moveDir.z != 0f && CanMove(_moveDir, distance);
    //         }
    //     }

    //     if(canMove){
    //         // lastMoveDir = moveDir;
    //         sm.transform.position += _moveDir * distance;
    //         Quaternion desiredRotation = Quaternion.LookRotation(_moveDir, Vector3.up);
    //         sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, 800f * Time.deltaTime);
    //         //Debug.Log("Moving...");
    //         return true;
    //     } else{
    //         return false;
    //     }
    // }

    // private bool CanMove(Vector3 dir, float distance){
    //     return Physics2D.Raycast(sm.transform.position, dir, distance).collider == null;
    // }

}
