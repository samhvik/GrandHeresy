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
using UnityEngine.SceneManagement;

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
    public GameObject[] players = new GameObject[4];
    //
    // Holds if player is on a controller or keyboard (Stored with names "keyboard" or "controller"
    public string[] whatGamepad = new string[4];
    //
    // Position of all players, used for Camera Controller
    public Transform[] playerPosition = new Transform[4];
    //
    // Get player kills
    public int[] playerKills = new int[4];
    //
    // Check to see if the player is alive or not
    public bool[] playerAlive = new bool[4];
    public int numAlive;
    //
    // Holds the Keyboard players cursor
    public GameObject[] playerCursors = new GameObject[4];
    public bool cursorLock = false;

    public int objectivesTotal;
    public int objectivesCompleted;

    public bool extractionStarted = false;
    public bool extractionOpen = false;
    public bool allPlayersInExtractionRange = false;
    public float extractionTimer;
    void Start(){
        playerHealth = 100.0f;
        playerSpeedWalk = 10.0f;
        playerSpeedAim = 7.0f;
        playerSpeedRun = 16.0f;

        // Initializing player variables
        for(int i = 0; i < 4; i++)
        {
            playerPosition[i] = this.transform;
            playerAlive[i] = false;

        }
        numPlayers = 0;
        numAlive = 0;

        // Uncomment this later, giving errors in my own scene
        // may need a better way to implement this so people can do stuff in their own scene
        winText = GameObject.Find("Win Text").GetComponent<Text>();

        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    void Awake(){
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    void Update(){
        // "end" game on player death
        for(int i = 0; i < numPlayers; i++) {
            var playerInventory = players[i].GetComponent<PlayerInventory>();
            if (playerInventory.health < 0){
                playerAlive[i] = false;
                numAlive -= 1;
            }
            if(findWhosAlive() < 0) { //Debug.Log("Player Death");
                SceneManager.LoadScene("Justin's Recap"); 
            }
            playerKills[i] = playerInventory.getKills();
        }
        // Allow 'esc' key to exit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Set Cursor to be visible
            Cursor.visible = true;
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
        return players[index];
    }

    public void GameCompleted(){
        winText.text = "All objectives complete, proceed to extract.";
    }

    // Returns the only player that is alive
    public int findWhosAlive()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            if (playerAlive[i] == true)
            {
                return i;
            }
        }

        return -1;
    }
}
