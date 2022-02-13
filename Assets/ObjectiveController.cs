using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script controls the spawn locations of objectives
public class ObjectiveController : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject[] spawnsSW;
    public GameObject[] spawnsSE;
    public GameObject[] spawnsNE;
    public GameObject[] spawnsNW;
    public GameObject[] chosenSpawns;
    public GameObject objective1ToSpawn;
    List<GameObject> spawnedObjects = new List<GameObject>();
    void Awake()
    {
        //objective1ToSpawn = (GameObject)Resources.Load("name of prefab to spawn", typeof(GameObject));
       objective1ToSpawn = (GameObject)Resources.Load("Tree",typeof(GameObject));
       //choose spawn groups
       chosenSpawns = chooseSpawnGroup();
       //go through chosen spawns and instantiate at each point 
       foreach (GameObject spawnPoint in chosenSpawns){
           objectSpawned = GameObject.Instantiate(objective1ToSpawn, spawnPoint.transform.position, Quaternion.identity);
           spawnedObjects.Add(objectSpawned);
           print("object should be spawned");
           print(spawnPoint.transform.position);
           print(spawnPoint.name);
       }
       
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    /*
       GameObject[] getSpawnPointsByGroup( string TargetGroup):
        Returns null if parameter not specified, else array of GameObjectPoints used for objective spawning
        Points are specified by acceptable parameter strings: SW, SE, NW, NE
    */
    GameObject[] getSpawnPointsByGroup( string TargetGroup){
        switch( TargetGroup )
        {
            case "SW":
                return GameObject.FindGameObjectsWithTag("ObjectiveSpawnSW");                
            case "SE":
                return GameObject.FindGameObjectsWithTag("ObjectiveSpawnSE");
            case "NW":
                return GameObject.FindGameObjectsWithTag("ObjectiveSpawnNW");
            case "NE":
                return GameObject.FindGameObjectsWithTag("ObjectiveSpawnNE");
            default:
                print("no groups specified");
                break;
        }
        return null;
    }
    GameObject[] chooseSpawnGroup(){
        spawnsSW = getSpawnPointsByGroup("SW");
        spawnsSE = getSpawnPointsByGroup("SE");
        spawnsNW = getSpawnPointsByGroup("NW");
        spawnsNE = getSpawnPointsByGroup("NE");
        int spawnDest = Random.Range(1,5);
        switch( spawnDest ){
            case 1:
                print("chose SW");
                return spawnsSW;
            case 2: 
                print("chose SE");
                return spawnsSE;
            case 3: 
                print("chose NW");
                return spawnsNW;
            case 4: 
                print("chose NE");
                return spawnsNE;
            default: 
                return spawnsSW;
        }
    }
    public GameObject[] getChosenSpawn(){
        if(chosenSpawns != null){
            return chosenSpawns;
        }
        else{
            print("no spawn group chosen yet");
            return null;
        }
    }
}
