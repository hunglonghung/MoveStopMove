using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim("run");
    }

    public void OnExecute(Character t)
    {
        // ((Player)t).CheckWin(((Player)t).bots);
        // if(t.isWin) t.ChangeState(new WinState());
        // if(t.isDead) t.ChangeState(new LoseState());
        //Player 
        ((Player)t).GetMoveDirection();
        if(!((Player)t).GetInput()) t.ChangeState(new IdleState());
        else ((Player)t).Move(((Player)t).MoveDirection);
        //Bot
        if(((Bot)t).GetComponent<NavMeshAgent>().pathPending == false) t.ChangeState(new IdleState());


    }

    public void OnExit(Character t)
    {

    }

}
