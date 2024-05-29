using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : IState<StateMachine>
{
    Character character;
    public void OnEnter(StateMachine t)
    {
        character = t.GetComponent<Character>();
        character.ChangeAnim("dance");
    }

    public void OnExecute(StateMachine t)
    {

    }

    public void OnExit(StateMachine t)
    {

    }

}
