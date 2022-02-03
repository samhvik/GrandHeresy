using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask; // this would be a player tag or something else denoting a player
    public LayerMask obstacleMask; // if we had specific obstacles, but can leave on "everything"
 
    public List<Transform> visibleTargets = new List<Transform>();

    void Start(){
        StartCoroutine("FindTargetsOnDelay", 0.3f);
    }

    // Tutorial Recommended delay on execution
    IEnumerator FindTargetsOnDelay(float delay){
        while(true){
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }


    void FindVisibleTargets(){
        // Kill all previous target tranforms
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++){
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2){
                float distToTarget = Vector3.Distance(transform.position, target.position);
                // add all visible units to the list of things to chase
                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask)){
                    visibleTargets.Add(target);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDeg, bool isGlobalAngle){
        // get the direction from the current angle the AI is looking in
        // trig stuff: angle - 90 degrees gives us the proper direction within a unit circle
        if(!isGlobalAngle){ angleInDeg += transform.eulerAngles.y;   }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }
}
