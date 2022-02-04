/*using System.Collections;
using UnityEngine;
using UnityEditor;

// AIFOV is our Field of View Script
// This Editor allows us to directly manipulate and debug in
// our editor
[CustomEditor (typeof (AIFOV))]
public class FieldofViewEditor : Editor
{
    void OnSceneGUI(){
        AIFOV fov = (AIFOV) target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle( fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        foreach(Transform visibleTarget in fov.visibleTargets){
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}*/
