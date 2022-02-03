using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    float snappedHorizontal;
    float snappedVertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleAnimatorValues(float horizontalMovement = 0.0f, float verticalMovement = 0.0f, 
                                        float horizontalAiming = 0.0f, float verticalAiming = 0.0f,
                                            bool isRunning = false)
    {   
        //Snapping the horizontal movement so its always a whole number
        //Can change this later to include a slower walk

        if(horizontalMovement > 0){
            snappedHorizontal = 1;
        }
        else if(horizontalMovement < 0){
            snappedHorizontal = -1;
        }
        else{
            snappedHorizontal = 0;
        }

        //Snapping the vertical movement so its always a whole number
        //Can change this later to include a slower walk

        if(verticalMovement > 0){
            snappedVertical = 1;
        }

        else if(verticalMovement < 0){
            snappedVertical = -1;
        }

        else{
            snappedVertical = 0;
        }

        // Getting the magnitude of the left stick axis to determine whether to walk, run, or sprint
        float mag = Mathf.Max(Mathf.Abs(verticalMovement), Mathf.Abs(horizontalMovement));
        if(isRunning){
            mag += 0.5f;
        }

        animator.SetBool("IsSprinting", isRunning);

        if(horizontalAiming != 0 || verticalAiming != 0){
            animator.SetBool("IsStrafing", true);
        }
        else{
            animator.SetBool("IsStrafing", false);
        }
    
        
        animator.SetFloat("InputMagnitude", mag, 0.1f, Time.deltaTime);
        animator.SetFloat("InputHorizontal", horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat("InputVertical", verticalMovement, 0.1f, Time.deltaTime);

        animator.SetFloat("AimInputHorizontal", horizontalAiming, 0.1f, Time.deltaTime);
        animator.SetFloat("AimInputVertical", verticalAiming, 0.1f, Time.deltaTime);
    }

}
