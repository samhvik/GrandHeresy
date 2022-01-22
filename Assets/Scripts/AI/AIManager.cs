using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public List<Transform> waypoints;
    private AIStateController[] enemies; // should change down the line to use less resources finding stuff
    /* Ideas for changing above
    -- Have the actual game manager hold an array of enemies and then just grab the game objects
    -- similar to above but have it be not attached to game manager object for "cleanliness"
    -- ????
    */

    private void Start(){
        enemies = FindObjectsOfType<AIStateController>();
        foreach(var e in enemies){
            e.SetupAI(true, waypoints);
        }
    }

    // more to add later...
}
