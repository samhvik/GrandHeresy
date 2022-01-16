using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Complete  -> figure out what this is
public class AIStateController : MonoBehaviour
{
    public AIStates currState;
    public EnemyStats enemyStats; // Enemy Base Class create this next time
    public Transform eyes;
    public AIStates remainState;

    // hidden inspector public variables for accessing
    // we dont wanna touch these in the editor
    [HideInInspector] public NavMeshAgent navMeshAgent;
    //[HideInInspector] public Complete.TankShooting tankShooting;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;

    private bool active;

    void Awake(){
        // get component from Complete.TankShooting
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if(!active)
            return;
        currState.UpdateState(this);
    }

    public void SetupAI(bool activationFromAIManager, List<Transform> waypointsFromAIManager){
        waypointsFromAIManager = waypointsFromAIManager;
        active = activationFromAIManager;

        if(active) { navMeshAgent.enabled = true; }
        else { navMeshAgent.enabled = false; }
    }

    public void TransitionToState(AIStates next){
        if(next != remainState){
            currState = next;
            OnExitState();
        }
    }

    public bool CheckIfCountdownElapse(float duration){
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState(){
        stateTimeElapsed = 0;
    }

    void OnDrawGizmos()
    {
        if (currState != null && eyes != null) {
            Gizmos.color = currState.sceneGizmoColor;
            Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
        }
    }
}
