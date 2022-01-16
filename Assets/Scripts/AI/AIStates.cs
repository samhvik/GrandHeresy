using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/States")]
public class AIStates : ScriptableObject
{
    public AIAction[] actions;
    public AITransition[] transitions;
    public Color sceneGizmoColor = Color.grey;

    // Update is called once per frame
    public void UpdateState(AIStateController controller)
    {
        DoAction(controller);
        CheckTransitions(controller);
    }

    private void DoAction(AIStateController controller){
        for(int i = 0; i < actions.Length; i++)
            actions[i].Act(controller);
    }

    private void CheckTransitions(AIStateController controller){
        for(int i = 0; i < transitions.Length; i++){
            bool decisionSuccess = transitions[i].decision.Decide(controller);
        
            if(decisionSuccess){
                controller.TransitionToState(transitions[i].trueState);
            } else {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
