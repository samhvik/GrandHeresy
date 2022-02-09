using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStateController : MonoBehaviour
{
    public AIStates currState;
    public EnemyStats enemyStats; // Enemy Base Class create this next time
    //public Transform eyes;
    public AIStates remainState;

    // hidden inspector public variables for accessing
    // we dont wanna touch these in the editor
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Vector3 lastKnownLocation;
    [HideInInspector] public float stateTimeElapsed;

    private bool active;

    void Awake(){
        // get ai components
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        if(active) { navMeshAgent.enabled = true; }
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

    // reset when exiting, a built-in function
    private void OnExitState(){
        stateTimeElapsed = 0;
    }

    // built in debugging "gizmo"
    /* turn on if FOV is not used anymore
    void OnDrawGizmos()
    {
        if (currState != null && eyes != null) {
            Gizmos.color = currState.sceneGizmoColor;
            Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
        }
    }*/
}
