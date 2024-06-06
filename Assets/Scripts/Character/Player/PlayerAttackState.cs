using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttackState : IState<Character>
{

    public void OnEnter(Character t)
    {
        t.ChangeAnim("attack");

    }

    public void OnExecute(Character t)
    {
        if(t.isDead) 
        {
            t.ChangeState(new PlayerLoseState());
        }
        else
        {
            t.objectScan();
            ((Player)t).GetMoveDirection();//Player
            if(((Player)t).GetInput())
            {
                t.ChangeState(new PlayerRunState());
            }
            else
            {
                AnimatorStateInfo stateInfo = t.anim.GetCurrentAnimatorStateInfo(0);
                if(t.CheckTarget(t.hitColliders) == 0|| BulletPool.Instance.IsBulletActive(t.WeaponType,t))
                {
                    t.ChangeState(new PlayerIdleState());
                }
                else
                {
                    t.LookAtEnemy();
                    
                    if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 0.5f)
                    {
                        // Animation "attack" has finished
                        t.Fire();
                    }
                    
                }
            }
        }
        
    }

    public void OnExit(Character t)
    {

    }
}
