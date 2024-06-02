using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerIdleState : IState<Character>
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
        // if(t.isWin) t.ChangeState(new WinState());
        // if(t.isDead) t.ChangeState(new LoseState());
        //Player
        ((Player)t).GetMoveDirection();
        ((Player)t).objectScan();
        if(((Player)t).GetInput()) t.ChangeState(new PlayerRunState());
        else if(t.CheckTarget(t.hitColliders) >=2 ) t.ChangeState(new PlayerAttackState());
    }

    public void OnExit(Character t)
    {

    }
    

}