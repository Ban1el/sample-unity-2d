using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Core : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;
    public GroundSensor groundSensor;
    public StateMachine machine;
    public State state => machine.state;

    public void SetupInstances()
    {
        machine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        {
            state.SetCore(this);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying || machine == null || state == null)
            return;

        List<State> states = machine.GetActiveStateBranch();

        if (states == null)
            return;

        string stateNames = string.Join(
            " > ",
            states.ConvertAll(s => s.GetType().Name)
        );

        GUIStyle style = new GUIStyle(EditorStyles.label); // Base it on an existing style
        style.fontSize = 25;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        UnityEditor.Handles.Label(
            transform.position + Vector3.up * 2f,
            "Active States: " + stateNames,
            style // Pass the style in here!
        );
#endif
    }
}
