using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState<Character>
{

    public void OnEnter(Character t)
    {
        t.ChangeAnim("idle");
    }

    public void OnExecute(Character t)
    {
        //Win lost check
        // if((Player)t != null)
        // {
        //     ((Player)t).CheckWin(((Player)t).bots);
        // }
        if(t.isWin) t.ChangeState(new WinState());
        if(t.isDead) t.ChangeState(new LoseState());
        //Player
        ((Player)t).GetMoveDirection();
        ((Player)t).objectScan();
        if(((Player)t).GetInput()) t.ChangeState(new RunState());
        else if(t.CheckTarget(t.hitColliders) == true) t.ChangeState(new AttackState());
        //Bot
        if(((Bot)t).GetComponent<NavMeshAgent>().pathPending == true) t.ChangeState(new RunState());
        else if(t.CheckTarget(t.hitColliders) == true) t.ChangeState(new AttackState());
    }

    public void OnExit(Character t)
    {

    }
    

}