using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Died")]
public class AIDiedAction : AIAction
{
    public override void Act(AIStateController controller)
    {
        Died (controller);
    }

    // travel between set waypoints that are fed from the AI Manager
    private void Died(AIStateController controller)
    {
        Debug.Log("Died");
    }
}