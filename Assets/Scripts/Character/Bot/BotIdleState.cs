using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotIdleState : IState<Character>
{
    float time;
    float randomTime;
    public void OnEnter(Character t)
    {
        t.ChangeAnim("idle");
        time = 0;
        randomTime = Random.Range(1,5); 
    }

    public void OnExecute(Character t)
    {
        if(t.isDead) t.ChangeState(new BotLoseState());
        else
        {
            time += Time.deltaTime;
            ((Bot)t).StopMoving();
            t.objectScan();
            if(((Bot)t).CheckPathPending() && time > randomTime)
            {
                t.ChangeState(new BotRunState());
            } 
            else if(t.CheckTarget(t.hitColliders) >= 1 && !BulletPool.Instance.IsBulletActive(t))
            {
                t.ChangeState(new BotAttackState());
            } 
        }
        
    }

    public void OnExit(Character t)
    {

    }
    

}