using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Character>
{

    public void OnEnter(Character t)
    {
        // t.ChangeAnim("idle");
    }

    public void OnExecute(Character t)
    {
        ((Player)t).GetMoveDirection();
        ((Player)t).objectScan();
        if(((Player)t).GetInput()) t.ChangeState(new RunState());
        else if(((Player)t).CheckTarget(((Player)t).hitColliders) == true) t.ChangeState(new AttackState());
    }

    public void OnExit(Character t)
    {

    }
    

}