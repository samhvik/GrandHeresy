using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateController : MonoBehaviour
{
    public AIStates currState;
    public AIStates remainState;
    public EnemyStats enemyStats; // Enemy Base Class create this next time
    public AudioSource aSource;
    public AudioClip[] enemySounds;

    // hidden inspector public variables for accessing
    // we dont wanna touch these in the editor
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Vector3 lastKnownLocation;
    [HideInInspector] public float stateTimeElapsed;

    private bool active;


    // Holds the Enemy Spawning Particles
    public ParticleSystem[] enemySpawn = new ParticleSystem[5];

    void Awake(){
        // get ai components
        navMeshAgent = GetComponent<NavMeshAgent>();
        aSource = GetComponent<AudioSource>();

        // Playing our Partilce System
        for (int i = 0; i < 5; i++)
        {
            Instantiate(enemySpawn[i], new Vector3(this.transform.position.x, this.transform.position.y - 0.95f, this.transform.position.z), this.transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
        }
    }

    void Update() {
        if(!active)
            return;
        currState.UpdateState(this);
    }

    // The AI Manager should call this on Start() to setup each AI individually
    public void SetupAI(bool activateAI, List<Transform> waypointsFromAIManager){
        wayPointList = waypointsFromAIManager;
        active = activateAI;
        // setup the NavmeshAgent
        if(active) { 
            navMeshAgent.enabled = true; 
            navMeshAgent.speed = enemyStats.moveSpeed;
            navMeshAgent.angularSpeed = enemyStats.searchTurnSpeed * 2f; // I *2 because otherwise its search speed is too fast
            this.GetComponent<AIFOV>().viewAngle  = enemyStats.lookAngle;
            this.GetComponent<AIFOV>().viewRadius = enemyStats.lookRadius;
        }
        else { navMeshAgent.enabled = false; }
    }

    // transition to the next state
    // this function is called after checking conditions explicitly
    // the "Current State" of the AI would call this
    public void TransitionToState(AIStates next){
        //Debug.Log("Transition to: " + next);
        if(next != remainState){
            currState = next;
            OnExitState();
        }
    }

    // can be used to as an attack cooldown etc.
    public bool CheckIfCountdownElapse(float duration){
        stateTimeElapsed += Time.deltaTime;
        //Debug.Log("Time Elapsed: " + (stateTimeElapsed >= duration));
        return (stateTimeElapsed >= duration);
    }

    // check if the AI is within attacking distance of the player its chasing
    public bool checkRange(){
        float dist = Vector3.Distance(chaseTarget.position, this.transform.position);
        //if(dist < enemyStats.attackRange){Debug.Log("CheckRange: " + dist);}
        return (dist <= enemyStats.attackRange);
    }

    // reset when exiting, a built-in function
    private void OnExitState(){
        stateTimeElapsed = 0;
    }

    public void Combat(bool status){
        GameValues.inCombatStatus = status;
        //Debug.Log("Combat Status Changed: " + GameValues.inCombatStatus);
    }
}
