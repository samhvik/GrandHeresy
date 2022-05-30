/*
    Target.cs

    Allows for rounds fired from a gun to damage the gameObject.
*/

using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class Target : MonoBehaviour
{

    public float health;
    public int numberOfDeathAnimations;
    public float timeTillDestroy;
    //public float target_DriveMaterial;
    //public float speed_DriveMaterial = 0.1f;
    private Animator animator;
    private AIStateController controller;
    private SkinnedMeshRenderer renderer;

    private float shaderValue = 1f;
    [SerializeField]
    private float dissolveStartDelayMin = 3f;
    [SerializeField]
    private float dissolveStartDelayMax = 3f;
    [SerializeField]
    private float change = 0.1f;
    [SerializeField]
    private float dissolveDelay = 0.2f;

    public void Awake()
    {
        //numberOfDeathAnimations -= 1;
        animator = GetComponent<Animator>();
        controller = GetComponent<AIStateController>();
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
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
        //renderer.material.SetFloat("_DriveMaterial", 0.5f);
        StartCoroutine(ChangeShader());

    }

    public int getRandomInt(int numberOfDeathAnimations)
    {
        System.Random rnd = new System.Random();
        int number = rnd.Next(0, numberOfDeathAnimations);
        Debug.Log("Death Anim num: " + number);
        return number;
    }

    public void dissolveEnemy()
    {
        // var d_current_float = renderer.material.GetFloat("_DriveMaterial");
        // var d_current_float = d_start_float;

        // while(d_current_float < target_DriveMaterial)
        // {
        //     renderer.material.SetFloat("_DriveMaterial", d_current_float);
        //     d_current_float += speed_DriveMaterial;
        // }

        // Mathf.Lerp(renderer.material.GetFloat("_DriveMaterial"), renderer.material.SetFloat("_DriveMaterial", d_current_float), 0.5);
    }

    public IEnumerator ChangeShader()
    {

        shaderValue = 0f;
        
        yield return new WaitForSeconds(UnityEngine.Random.Range(dissolveStartDelayMin, dissolveStartDelayMax));
        //yield return new WaitForSeconds(NextFloat(dissolveStartDelayMin, dissolveStartDelayMin));

        while (shaderValue < 1f){

            yield return new WaitForSeconds(dissolveDelay);             

            //if (this.GetComponentInChildren<SkinnedMeshRenderer>().material = changeDisolve) {
            shaderValue += change;
            renderer.material.SetFloat("_DriveMaterial", shaderValue);
            //}
        }
    }

    public float NextFloat(float min, float max){
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
    
}
