/*
    GameValues.cs

    Stores all relavent game data during a game instance.
    
    Many scripts may reference this to obtain information 
    about the current game state.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameValues : MonoBehaviour{

    public static string level;
    
    public static GameValues instance = null;

    public float playerHealth;
    public float playerSpeedWalk;
    public float playerSpeedAim;
    public float playerSpeedRun;
    private Text winText;

    /// <summary>
    /// Handling Player
    /// </summary>
    /// 
    // # of players in the game currently
    public int numPlayers;
    //
    // Holds our Players to be called upon
    public GameObject[] Players = new GameObject[4];
    //
    // Position of all players, used for Camera Controller
    public Transform[] playerPosition = new Transform[4];

    public int objectivesTotal;
    public int objectivesCompleted;

    void Start(){
        playerHealth = 100.0f;
        playerSpeedWalk = 15.0f;
        playerSpeedAim = 10.0f;
        playerSpeedRun = 25.0f;

        numPlayers = 0;

        winText = GameObject.Find("Win Text").GetComponent<Text>();
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

        if(objectivesCompleted < objectivesTotal)
            winText.text = "Objectives Completed: " + objectivesCompleted;

    }

    public void UpdateHealth(int damage){
        playerHealth -= damage;
    }

    // Returns how many players are in the game currently
    public int getNumPlayers()
    {
        return numPlayers;
    }

    // Returns the gameobject of the player with the given index
    public GameObject getPlayer(int index)
    {
        return Players[index];
    }

    public void GameCompleted(){
        winText.text = "Game Over, You Win!";
    }
}
