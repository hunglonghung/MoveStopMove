using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackState : IState<Character>
{

    public void OnEnter(Character t)
    {
        t.ChangeAnim("attack");

    }

    public void OnExecute(Character t)
    {
        // ((Player)t).CheckWin(((Player)t).bots);
        // if(t.isWin) t.ChangeState(new WinState());
        // if(t.isDead) t.ChangeState(new LoseState());
        t.objectScan();
        ((Player)t).GetMoveDirection();//Player
        if(((Player)t).GetInput())
        {
            t.ChangeState(new RunState());
        }
        else
        {
            AnimatorStateInfo stateInfo = t.anim.GetCurrentAnimatorStateInfo(0);
            if(t.hitColliders.Count() == 1 || BulletPool.Instance.IsBulletActive())
            {
                t.ChangeState(new IdleState());
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
