using UnityEngine;

public class Walking : Moving
{
    private float L_horizontalInput;
    private float L_verticalInput;
    private float R_horizontalInput;
    private float R_verticalInput;

    public Walking(MovementSM stateMachine) : base("Walking", stateMachine) { }

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

        Vector3 moveDir = new Vector3(L_horizontalInput, 0, L_verticalInput).normalized;
        Debug.DrawLine(sm.transform.position, sm.transform.position + moveDir*2, Color.green);

        // Move the character depending on stick direction
        sm.GetComponent<CharacterController>().SimpleMove(new Vector3(
            L_horizontalInput * GameValues.instance.playerSpeedWalk,
            0.0f,
            L_verticalInput * GameValues.instance.playerSpeedWalk
        ));

        // -------------- New way to rotate character w/ lookSpeed variable -------
        sm.faceDirection = new Vector3(L_horizontalInput, 0 , L_verticalInput);
        sm.faceDirection.Normalize();

        sm.transform.Translate(sm.faceDirection * Time.deltaTime, Space.World);
        if(sm.faceDirection != Vector3.zero){
            Quaternion toRotation = Quaternion.LookRotation(sm.faceDirection, Vector3.up);
            sm.transform.rotation = Quaternion.RotateTowards(sm.transform.rotation, toRotation, sm.lookSpeed * Time.deltaTime);
        }

        // If you aim while walking, switch to strafe state
        if(R_horizontalInput != 0 || R_verticalInput != 0 || sm.isAiming == true){
            stateMachine.ChangeState(sm.strafeAimState);
        }
    }
}