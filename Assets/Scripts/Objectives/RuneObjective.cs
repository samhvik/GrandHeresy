/*
    RuneObjective.cs

    Handles everything related to the "destory the rune" objective type.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneObjective : MonoBehaviour
{
    public float health;
    private Renderer mat;

    void Start(){
        mat = gameObject.GetComponent<Renderer>();
        //mat.material.color = Color.black;
    }

    void Update(){
        if(health <= 0){
            if(GameValues.instance.objectivesCompleted == GameValues.instance.objectivesTotal){
                GameValues.instance.GameCompleted();
            }
        }
    }

    public void Hit(float damage){
        if(health > 0){
            health -= damage;
            Debug.Log("Hit Objective");

            // Destroy event when health is less than zero
            if(health <= 0) RuneDestroyed();

            // enable combat for enemy spawning
            GameValues.inCombatStatus = true;
        }
    }

    public void RuneDestroyed()
    {
        mat.material.color = Color.green;
        GameValues.instance.objectivesCompleted++;
    }
}
