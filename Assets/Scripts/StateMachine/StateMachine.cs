using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private IState<StateMachine> currentState;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == null) return;
        Debug.Log(currentState);
        currentState.OnExecute(this);
    
        
    }
    //Change State

    public void ChangeState(IState<StateMachine> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    


}