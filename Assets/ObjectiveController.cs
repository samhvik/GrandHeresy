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
    //should be set within inspector to some gameobject (default is a tree)
    public GameObject objective1ToSpawn;

    public GameObject[] objectivesToSpawn;
    private GameObject objectSpawned;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    void Awake()
    {
        //objective1ToSpawn = (GameObject)Resources.Load("name of prefab to spawn", typeof(GameObject));
        //why we have to use resources folder: https://answers.unity.com/questions/1170405/resourcesload-without-a-resources-folder.html
        //defaults to a tree if no objective is specified
       if(objective1ToSpawn == null){
           print("this shit is null");
           objective1ToSpawn = (GameObject)Resources.Load("Tree",typeof(GameObject));
       }
       
       //choose spawn groups
       chosenSpawns = chooseSpawnGroup();
       //go through chosen spawns and instantiate at each point 
       //currently only handles spawning one objective type
       foreach (GameObject spawnPoint in chosenSpawns){
           objectSpawned = GameObject.Instantiate(objective1ToSpawn, spawnPoint.transform.position, Quaternion.identity);
           spawnedObjects.Add(objectSpawned);
           print(objectSpawned.name);
           print("object should be spawned");
           print(spawnPoint.transform.position);
           print(spawnPoint.name);
       }
       /*
        //picks a random objective from a list of objectives to spawn at a specified spawn point 
        if(objectivesToSpawn.Length >= 2){
            foreach(GameObject spawnPoint in chosenSpawns){
                int objSpawn = Random.Range(1,objectiveToSpawn.Length);
                objectSpawned = GameObject.Instantiate(objectivesToSpawn[objSpawn], spawnPoint.transform.position, Quaternion.identity);
                spawnedObjects.Add(objectSpawned);
            }
        }
       */
       
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
        helper method that returns null if parameter not specified, else array of GameObjectPoints used for objective spawning
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
    //Destroys and removes objectives completed from spawnedObjects list
    // void destroyObject( GameObject Obj){
    //     if(spawnedObjects.Find(Obj)){
    //         spawnedObjects.Remove(Obj);
    //         Destroy(Obj);
    //     }
        
    // }

    // void ObjectiveCompleted(GameObject Obj){
    //     //do necessary stuff for completing objectives 
    //     //destroy the objective now that its done 
    //     destroyObject(Obj);
    // }
}
