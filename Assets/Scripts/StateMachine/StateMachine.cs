using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }

    void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    public virtual string GetCurrentState(){
        if (currentState != null)
            return currentState.name;

        return "NULL STATE";
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    /* Temporarily Commenting out to test co-op canvas text
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(300f, 10f, 200f, 100f));
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='red'><size=40>{content}</size></color>");
        GUILayout.EndArea();
    }
    */
}