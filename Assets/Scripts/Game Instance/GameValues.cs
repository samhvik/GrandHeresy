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

    public static string level;
    
    public static GameValues instance = null;

    public float playerHealth;
    public float playerSpeedWalk;
    public float playerSpeedAim;
    public float playerSpeedRun;

    // # of players in the game currently
    public int numPlayers;

    // Position of all players, used for Camera Controller
    public Transform[] playerPosition = new Transform[4];

    void Start(){
        playerHealth = 100.0f;
        playerSpeedWalk = 10.0f;
        playerSpeedAim = 5.0f;
        playerSpeedRun = 15.0f;

        numPlayers = 0;
    }

    void Awake(){
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    void Update(){
        // "end" game on player death
        if(playerHealth < 0){
            Debug.Log("Player Death");
            // Just close the game on player death for now its week1
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
        // Allow 'esc' key to exit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }

    public void UpdateHealth(int damage){
        playerHealth -= damage;
    }
}
