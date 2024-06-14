using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerRunState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim("run");
    }

    public void OnExecute(Character t)
    {
        ((Player)t).GetMoveDirection();
        if(!((Player)t).GetInput()) t.ChangeState(new PlayerIdleState());
        else ((Player)t).Move(((Player)t).MoveDirection);
        
        
    
    }

    public void OnExit(Character t)
    {
        
    }

}
