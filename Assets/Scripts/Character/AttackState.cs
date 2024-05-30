using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Character>
{
    float time;
    public void OnEnter(Character t)
    {
        t.ChangeAnim("attack");
        time = 0f;
    }

    public void OnExecute(Character t)
    {
        ((Player)t).GetMoveDirection();
        if(((Player)t).GetInput())
        {
            t.ChangeState(new RunState());
        }
        else
        {
            if(BulletPool.Instance.IsBulletActive())
            {
                if(time > 1.5f) t.ChangeState(new IdleState());
                
            }
            else
            {
                t.anim.ResetTrigger("attack");
                t.anim.SetTrigger("attack");
                ((Player)t).Fire();
                time += Time.deltaTime;
            }
        }
        
    }

    public void OnExit(Character t)
    {

    }

}
