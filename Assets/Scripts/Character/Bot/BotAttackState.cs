using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotAttackState : IState<Character>
{

    public void OnEnter(Character t)
    {
        t.ChangeAnim("attack");

    }

    public void OnExecute(Character t)
    {
        if(t.isDead) t.ChangeState(new BotLoseState());
        else
        {
            t.objectScan();
            ((Bot)t).StopMoving();
            AnimatorStateInfo stateInfo = t.anim.GetCurrentAnimatorStateInfo(0);
            if(t.CheckTarget(t.hitColliders) == 0 || BulletPool.Instance.IsBulletActive(t))
            {
                t.ChangeState(new BotIdleState());
            }
            else
            {
                t.LookAtEnemy();
                
                if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.8f)
                {
                    // Animation "attack" has finished
                    t.Fire();
                }
                
            }
        }
        
        
    }

    public void OnExit(Character t)
    {

    }
}
