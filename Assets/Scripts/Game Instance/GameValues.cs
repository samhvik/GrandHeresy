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
    public static bool inCombatStatus = false;

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
    // Holds if player is on a controller or keyboard (Stored with names "keyboard" or "controller"
    public string[] whatGamepad = new string[4];
    //
    // Position of all players, used for Camera Controller
    public Transform[] playerPosition = new Transform[4];

    public int objectivesTotal;
    public int objectivesCompleted;

    void Start(){
        playerHealth = 100.0f;
        playerSpeedWalk = 15.0f;
        playerSpeedAim = 10.0f;
        playerSpeedRun = 20.0f;

        // Initializing player variables
        for(int i = 0; i < 4; i++)
        {
            playerPosition[i] = this.transform;

        }
        numPlayers = 0;

        // Uncomment this later, giving errors in my own scene
        // may need a better way to implement this so people can do stuff in their own scene
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
            Debug.Log("A Player Died");
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit(); // change this to be how we want player death to interact
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
