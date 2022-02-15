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


    public Dodge (MovementSM stateMachine) : base("Dodge", stateMachine) {
        sm = (MovementSM) this.stateMachine;
    }

    public override void Enter(){
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        _slideSpeed = sm.slideSpeed;
        _slideFalloff = sm.slideFalloff;
    }

    public override void UpdateLogic(){
        base.UpdateLogic();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        moveDir = new Vector3(L_horizontalInput, 0, L_verticalInput).normalized;

        Debug.DrawLine(sm.transform.position, new Vector3(L_horizontalInput, 0, L_verticalInput), Color.green);

        sm.transform.position += moveDir * _slideSpeed * Time.deltaTime;
        _slideSpeed -= _slideSpeed * _slideFalloff * Time.deltaTime;

        // If the left stick is in motion, switch to walking state
        if(_slideSpeed < 5f){
            stateMachine.ChangeState(sm.idleState);
        }
    }

    //private bool TryMove(Vector3 baseMoveDir, float distance)

}
