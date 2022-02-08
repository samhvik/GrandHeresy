using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    float snappedHorizontal;
    float snappedVertical;
    private Vector3 moveDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleAnimatorValues(float h_Movement = 0.0f, float v_Movement = 0.0f, 
                                        float h_Aiming = 0.0f, float v_Aiming = 0.0f,
                                            bool isRunning = false)
    {   
        //Snapping the horizontal movement to a whole number
        //Could change this later to include slower walks
        // if(h_Movement > 0) snappedHorizontal = 1;
        // else if(h_Movement < 0) snappedHorizontal = -1;
        // else snappedHorizontal = 0;

        // if(v_Movement > 0) snappedVertical = 1;
        // else if(v_Movement < 0) snappedVertical = -1;
        // else snappedVertical = 0;        

        // Getting the magnitude of the left stick axis to determine whether to walk, run, or sprint
        float mag = Mathf.Ceil(Mathf.Max(Mathf.Abs(v_Movement), Mathf.Abs(h_Movement)));
        if(isRunning) mag *= 2f;
        animator.SetBool("IsSprinting", isRunning);

        // Checking whether in freelocomotion or strafe locomotion
        HandleLocomotionState(h_Aiming, v_Aiming);
        
        animator.SetFloat("InputMagnitude", mag, 0.1f, Time.deltaTime);
        
        // Animate Character Depending which mode they are in
        if(animator.GetBool("IsStrafing")){

            moveDirection = new Vector3(h_Movement, 0, v_Movement);
            if(moveDirection.magnitude > 1.0f){
                moveDirection = moveDirection.normalized;
            }
            moveDirection = transform.InverseTransformDirection(moveDirection);

            animator.SetFloat("InputHorizontal", moveDirection.x, 0.1f, Time.deltaTime);
            animator.SetFloat("InputVertical", moveDirection.z, 0.1f, Time.deltaTime);
        }
        else{

            animator.SetFloat("InputHorizontal", h_Movement, 0.1f, Time.deltaTime);
            animator.SetFloat("InputVertical", v_Movement, 0.1f, Time.deltaTime);
        }

        // Getting values for aiming with the right stick
        animator.SetFloat("AimInputHorizontal", h_Aiming, 0.1f, Time.deltaTime);
        animator.SetFloat("AimInputVertical", v_Aiming, 0.1f, Time.deltaTime);
    }

    private void HandleLocomotionState(float h_Aiming, float v_Aiming){
        if(h_Aiming != 0 || v_Aiming != 0) animator.SetBool("IsStrafing", true);
        else animator.SetBool("IsStrafing", false);
    }

}
