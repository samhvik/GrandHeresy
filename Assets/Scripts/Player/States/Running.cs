using UnityEngine;

public class Running : Moving
{
    private float L_horizontalInput;
    private float L_verticalInput;
    private float R_horizontalInput;
    private float R_verticalInput;

    public Running(MovementSM stateMachine) : base("Running", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        L_horizontalInput = sm.left_horizontal;
        L_verticalInput = sm.left_vertical;
        R_horizontalInput = sm.right_horizontal;
        R_verticalInput = sm.right_vertical;

        sm.animatorManager.HandleAnimatorValues(L_horizontalInput, L_verticalInput, R_horizontalInput, R_verticalInput, true);

        // Move the character depending on stick direction
        sm.GetComponent<CharacterController>().SimpleMove(new Vector3(
            L_horizontalInput * GameValues.instance.playerSpeedRun,
            0.0f,
            L_verticalInput * GameValues.instance.playerSpeedRun
        ));

        // -------------- New way to rotate character w/ lookSpeed variable -------
        sm.faceDirection = new Vector3(L_horizontalInput, 0 , L_verticalInput);
        sm.faceDirection.Normalize();

        sm.transform.Translate(sm.faceDirection * Time.deltaTime, Space.World);
        if(sm.faceDirection != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(sm.faceDirection, Vector3.up);
            sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, toRotation, sm.lookSpeed * Time.deltaTime);
        }

        // If left stick magnitude reaches a certain threshold, switch to walk state
        float mag = Mathf.Max(Mathf.Abs(L_verticalInput), Mathf.Abs(L_horizontalInput));
        if(mag < 0.5f){
            stateMachine.ChangeState(sm.walkingState);
        }

        // If right stick is moved, switch to strafe aim state
        if(R_horizontalInput != 0 || R_verticalInput != 0){
            stateMachine.ChangeState(sm.strafeAimState);
        }
    }

}