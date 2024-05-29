using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<StateMachine>
{
    Character character;
    public void OnEnter(StateMachine t)
    {
        character = t.GetComponent<Character>();
        character.ChangeAnim("attack");
    }

    public void OnExecute(StateMachine t)
    {

    }

    public void OnExit(StateMachine t)
    {

    }

}
