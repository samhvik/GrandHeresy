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
    private float _slideSpeed;
    private float _slideFalloff;

    //private float dodgeCoolDown = 1;
    //private float actCoolDown;


    public Dodge (MovementSM stateMachine) : base("Dodge", stateMachine) {
        sm = (MovementSM) this.stateMachine;
    }

    public override void Enter(){
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        _slideSpeed = sm.dodgeSlideSpeed;
        _slideFalloff = sm.dodgeSlideFalloff;

        moveDir = new Vector3(L_horizontalInput, 0, L_verticalInput).normalized;
        sm.animatorManager.HandleDodgeRollState(true);
        //sm.rigLayer_HandIK.weight = 0;
    }

    public override void UpdateLogic(){
        base.UpdateLogic();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        
        Debug.DrawLine(sm.transform.position, new Vector3(L_horizontalInput, 0, L_verticalInput), Color.green);
        
        //sm.transform.position += moveDir * _slideSpeed * Time.deltaTime;
        TryMove(moveDir, _slideSpeed * Time.deltaTime);
        _slideSpeed -= _slideSpeed * _slideFalloff * Time.deltaTime;

        // When slide speed is slowing down, switch back to idle state
        if(_slideSpeed < 0.3f){
            sm.animatorManager.HandleDodgeRollState(false);
            stateMachine.ChangeState(sm.walkingState);
        }
    }

    private bool TryMove(Vector3 baseMoveDir, float distance){
        Vector3 _moveDir = baseMoveDir;
        bool canMove = CanMove(_moveDir, distance);
        if(!canMove){
            _moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = _moveDir.x != 0f && CanMove(_moveDir, distance);
            if(!canMove){
                _moveDir = new Vector3(0f, baseMoveDir.z).normalized;
                canMove = _moveDir.z != 0f && CanMove(_moveDir, distance);
            }
        }

        if(canMove){
            // lastMoveDir = moveDir;
            sm.transform.position += _moveDir * distance;
            Quaternion desiredRotation = Quaternion.LookRotation(_moveDir, Vector3.up);
            sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, desiredRotation, 800f * Time.deltaTime);
            //Debug.Log("Moving...");
            return true;
        } else{
            return false;
        }
    }

    private bool CanMove(Vector3 dir, float distance){
        return Physics2D.Raycast(sm.transform.position, dir, distance).collider == null;
    }

}
