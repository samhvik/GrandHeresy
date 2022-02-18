/*
    RuneObjective.cs

    Handles everything related to the "destory the rune" objective type.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneObjective : MonoBehaviour
{
    private float health;
    private Renderer mat;

    void Start(){
        health = 50;
        mat = gameObject.GetComponent<Renderer>();
        mat.material.color = new Color(255f,255f,255f,0f);
    }

    void Update(){
        if(health <= 0){
            mat.material.color = new Color(0f,255f,0f,0f);
            GameValues.instance.objectivesCompleted++;

            if(GameValues.instance.objectivesCompleted == GameValues.instance.objectivesTotal){
                GameValues.instance.GameCompleted();
            }
            health = 99999999;
        }
    }

    public void Hit(float damage){
        health -= damage;
        Debug.Log("Hit Objective");
    }
}
