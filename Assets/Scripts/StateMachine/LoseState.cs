using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : IState<StateMachine>
{
    Character character;
    public void OnEnter(StateMachine t)
    {
        character = t.GetComponent<Character>();
        character.ChangeAnim("die");
    }

    public void OnExecute(StateMachine t)
    {

    }

    public void OnExit(StateMachine t)
    {

    }

}
