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

        ((Player)t).GetMoveDirection();
        if(((Player)t).GetInput()) t.ChangeState(new PlayerRunState());
        else if(t.CheckTarget(t.hitColliders) >=1 && !BulletPool.Instance.IsBulletActive(t.WeaponType,t))
        {
            ((Player)t).SetTarget();
            t.ChangeState(new PlayerAttackState());
        } 
            
        
        
    }

    public void OnExit(Character t)
    {

    }
    

}