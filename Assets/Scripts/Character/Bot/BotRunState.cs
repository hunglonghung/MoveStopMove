using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotRunState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim("run");
    }

    public void OnExecute(Character t)
    {
        ((Bot)t).UpdateNewPosition();
        if(!((Bot)t).CheckPathPending()) t.ChangeState(new BotIdleState());
    }

    public void OnExit(Character t)
    {

    }

}
