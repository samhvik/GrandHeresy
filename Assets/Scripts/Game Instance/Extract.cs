/*
    Extract.cs
    Handles the extraction procedure upon player interaction.
*/
/*
    How extraction should work:
    Before players load into the map, a beacon location will be randomly selected and a beacon
    prefab will spawn in its place. (Similar to objective spawns)
    Players will be able to extract when all objectives are either Completed or Failed.
    When this condition is met, players will now be able to interact with the extraction beacon.
    When the extraction beacon is triggered, this script is essentailly activated and all players are
    immediately detected.
    When triggered, a timer will initiate. The time shall be random, between 30 and 90 seconds.
    Players are expected to survive for the duration of the beacon's timer.
    The beacon will also have a set radius that, when a player leaves, will cause the current beacon
    procedure to completely stop. Players will have to re-interact with the beacon to restart the process.
    Upon the timer's completion, all players are "warped" back to HQ and the game is over.
    The players will then be taken to a recap screen that lets them know the run was completed,
    display in-game stats, etc. They will then have the option to return to the main menu/map selection.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using FMOD.Studio;

[System.Serializable]
public class Extract : MonoBehaviour
{
    public float extractionTime;
    // public bool extractionOpen = false;
    public GameObject[] beaconSpawns;
    // set in inspector to some gameObject (default is a tree)
    public GameObject beaconPrefab;
    private GameObject beaconSpawnPoint;
    private GameObject beaconSpawned;
    public GameObject TimerPanel;
    public Text timerText;
    public bool timerIsRunning = false;
    public float timeRemaining;
    //private int nextUpdate=1;
    private float elapsed = 0f;
    public Animator animator;
    
    void Awake(){
        TimerPanel.SetActive(false);

        if(beaconPrefab == null){
           beaconPrefab = (GameObject)Resources.Load("Tree",typeof(GameObject));
        }
        beaconSpawnPoint = chooseBeaconPoint();
        GameObject.Instantiate(beaconPrefab, beaconSpawnPoint.transform.position, Quaternion.identity);
        // extractionTime = Random.Range(30.0f, 90.0f) * Time.deltaTime;
        extractionTime = Mathf.Round(Random.Range(30.0f, 90.0f));
        // print("extractionTime = " + extractionTime);
        timeRemaining = extractionTime;
        GameValues.instance.extractionTimer = extractionTime;
    }
    void Start()
    {
        // if(beaconPrefab == null){
        //    beaconPrefab = (GameObject)Resources.Load("Tree",typeof(GameObject));
        // }
        // beaconSpawnPoint = chooseBeaconPoint();
        // GameObject.Instantiate(beaconPrefab, beaconSpawnPoint.transform.position, Quaternion.identity);
        // // extractionTime = Random.Range(30.0f, 90.0f) * Time.deltaTime;
        // extractionTime = Mathf.Round(Random.Range(30.0f, 90.0f));
        // // print("extractionTime = " + extractionTime);
        // timeRemaining = extractionTime;
        // GameValues.instance.extractionTimer = extractionTime;
        
    }

    void Update()
    {
        if (IsReached())
        {
            GameValues.instance.extractionOpen = true;
            TimerPanel.SetActive(true);
            timerText.text = ""+timeRemaining;
            //check if all players are inside range and if extraction has started before allowing timer to run
            if(GameValues.instance.allPlayersInExtractionRange && GameValues.instance.extractionStarted){
                timerIsRunning = true;    
            }
            else{
                timerIsRunning = false;
            }
        }

        if(timerIsRunning){
            // timerText.text = "Time Remaining: " + timeRemaining;
            timerText.text = ""+timeRemaining;
        }
        // else{
        //     timerText.text = "";
        // }
        //makes sure 1 second has passed before updating timer.
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            timerUpdate();
        }
        if(timeRemaining == 0){
            //change scene to recap scene
            //print("change to recap scene");
            // Fade.ToggleLevelFade(animator);
            SceneManager.LoadScene("Recap");
            //Fade.SwapLevelStr("Recap");
        }

    }
    /*
    * Chooses from a list of BeaconSpawnPoints to spawn a beacon for extraction at
    */
    GameObject chooseBeaconPoint(){
        beaconSpawns = getBeaconSpawnPoints();
        int spawnDest = Random.Range(1,4);
        switch(spawnDest){
            case 1:
                return beaconSpawns[0];
            case 2:
                return beaconSpawns[1];
            case 3: 
                return beaconSpawns[2];
            default:
                return beaconSpawns[0];
        }
            
    }
    /*
    * Gets the list of BeaconSpawnsPoints tagged with BeaconPoint
    */
    GameObject[] getBeaconSpawnPoints(){
        return GameObject.FindGameObjectsWithTag("BeaconPoint");
    }

    public bool IsReached()
    {
        return (GameValues.instance.objectivesCompleted >= GameValues.instance.objectivesTotal);
    }

    private void timerUpdate(){
        // Debug.Log("From timerUpdate" + Time.time);
        // print("From time is running " + timerIsRunning);
        if(timerIsRunning){
            if(timeRemaining > 0)
            {
                timeRemaining -= 1f;
                GameValues.instance.extractionTimer = timeRemaining;
                print(timeRemaining + " Time remaining");
            }
            else{ 
                Debug.Log("Timer is done");
                timeRemaining = 0;
                GameValues.instance.extractionTimer =timeRemaining;
                timerIsRunning = false; 
            }
        }
    }
}