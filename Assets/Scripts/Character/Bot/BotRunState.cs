using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotRunState : IState<Character>
{
    float time;
    float randomTime;
    public void OnEnter(Character t)
    {
        t.ChangeAnim("run");
        time = 0;
        randomTime = Random.Range(1.5f,6); 
    }

    public void OnExecute(Character t)
    {
        if(t.isDead) t.ChangeState(new BotLoseState());
        else
        {
            time += Time.deltaTime;
            ((Bot)t).UpdateNewPosition();
            ((Bot)t).StartMoving();
            if(!((Bot)t).CheckPathPending() || time >= randomTime) t.ChangeState(new BotIdleState());
        }
       
        
    }

    public void OnExit(Character t)
    {

    }

}
