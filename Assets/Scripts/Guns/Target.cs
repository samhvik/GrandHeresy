/*
    Target.cs

    Allows for rounds fired from a gun to damage the gameObject.
*/

using System;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour{

    public float health;
    public int numberOfDeathAnimations;
    public float timeTillDestroy;
    private Animator animator;
    private AIStateController controller;

    public void Awake()
    {
        //numberOfDeathAnimations -= 1;
        animator = GetComponent<Animator>();
        controller = GetComponent<AIStateController>();
    }

    public void Hit(float damage){
        health -= damage;
        if(health <= 0) KillObject();
        Debug.Log("Hit");
    }

    public void KillObject()
    {
        animator.SetInteger("DeathAnim", getRandomInt(numberOfDeathAnimations));
        animator.SetTrigger("HasDied");
        // controller.navMeshAgent.isStopped = true;
        Destroy(GetComponent<CapsuleCollider>());
        Destroy(GetComponent<AIStateController>());
        Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
        Destroy(this.gameObject, timeTillDestroy);
    }

    public int getRandomInt(int numberOfDeathAnimations)
    {
        System.Random rnd = new System.Random();
        int number = rnd.Next(0, numberOfDeathAnimations);
        Debug.Log("Death Anim num: " + number);
        return number;
    }
    
}
