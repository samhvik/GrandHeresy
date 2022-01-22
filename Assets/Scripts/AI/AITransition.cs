using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for transitioning between states
// inside the scriptable objects
[System.Serializable]
public class AITransition
{
    public AIDecision decision;
    public AIStates trueState;
    public AIStates falseState;
}
