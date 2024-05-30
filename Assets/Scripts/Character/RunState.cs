using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim("run");
    }

    public void OnExecute(Character t)
    {
        t.ChangeAnim("run");
        ((Player)t).GetMoveDirection();
        if(!((Player)t).GetInput()) t.ChangeState(new IdleState());
        else ((Player)t).Move(((Player)t).MoveDirection);

    }

    public void OnExit(Character t)
    {

    }

}
