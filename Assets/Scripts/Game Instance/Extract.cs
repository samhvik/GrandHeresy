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
    public bool timerIsRunning;
    public float timeRemaining;
    void Start()
    {
        if(beaconPrefab == null){
           beaconPrefab = (GameObject)Resources.Load("Tree",typeof(GameObject));
        }
        beaconSpawnPoint = chooseBeaconPoint();
        GameObject.Instantiate(beaconPrefab, beaconSpawnPoint.transform.position, Quaternion.identity);
        extractionTime = Random.Range(30.0f, 90.0f) * Time.deltaTime;

    }

    void Update()
    {
         if (IsReached())
        {
            GameValues.instance.extractionOpen = true;
            timeRemaining = extractionTime;

            //allow players to interact with beacon ask quinn how to do done
            //check if beacon is active (after being interacted with) done
            //check if all players are in the collider done
            if(GameValues.instance.allPlayersInExtractionRange && GameValues.instance.extractionStarted){
                timerIsRunning = true;    
            }
        }
        
        else{
            timerIsRunning = false; 
        }
        if(timerIsRunning){
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                print(timeRemaining);
            }
            else{ 
                Debug.Log("Timer is done");
                timeRemaining = 0;
                timerIsRunning = false; 
                //this is where we would change scenes to the recap scene
            }
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

    public void Extraction()
    {
        if (IsReached())
        {
            GameValues.instance.extractionOpen = true;
            extractionTime = Random.Range(30.0f, 90.0f) * Time.deltaTime;
        }
    }
}