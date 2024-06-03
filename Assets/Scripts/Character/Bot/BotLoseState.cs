using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BotLoseState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim("dead");
        ((Bot)t).StopMoving();
    }

    public void OnExecute(Character t)
    {
        
        AnimatorStateInfo stateInfo = t.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Dead") && stateInfo.normalizedTime >= 1)
        {
            Debug.Log(((Bot)t).BotSpawner.SpawnedBotList.Remove(t.gameObject));
            ((Bot)t).BotSpawner.SpawnedBotList.Remove(t.gameObject);
            t.OnDestroy();
            
        }
    }

    public void OnExit(Character t)
    {

    }
}
