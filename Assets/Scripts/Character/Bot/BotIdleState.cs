using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotIdleState : IState<Character>
{
    float time;
    public void OnEnter(Character t)
    {
        t.ChangeAnim("idle");
        time = 0;
    }

    public void OnExecute(Character t)
    {
        time += Time.deltaTime;
        t.objectScan();
        if(((Bot)t).CheckPathPending() && time > Random.Range(1.5f,4))
        {
            t.ChangeState(new BotRunState());
        } 
        else if(t.CheckTarget(t.hitColliders) >= 2 ) t.ChangeState(new BotAttackState());
    }

    public void OnExit(Character t)
    {

    }
    

}