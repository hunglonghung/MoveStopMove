using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<StateMachine>
{
    Character character;
    public void OnEnter(StateMachine t)
    {
        character = t.GetComponent<Character>();
        character.ChangeAnim("idle");
    }

    public void OnExecute(StateMachine t)
    {

        
    }

    public void OnExit(StateMachine t)
    {

    }
    

}