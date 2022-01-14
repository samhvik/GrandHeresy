/*
    GameValues.cs

    Stores all relavent game data during a game instance.
    
    Many scripts may reference this to obtain information 
    about the current game state.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour{
    
    public static GameValues instance = null;

    public float playerHealth;
    public float playerSpeedWalk;
    public float playerSpeedAim;
    public float playerSpeedRun;


    void Start(){
        playerHealth = 100.0f;
        playerSpeedWalk = 10.0f;
        playerSpeedAim = 5.0f;
        playerSpeedRun = 15.0f;
    }

    void Awake(){
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }
}
