using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoseState : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.ChangeAnim("dead");
    }

    public void OnExecute(Character t)
    {
        
    }

    public void OnExit(Character t)
    {

    }

}
