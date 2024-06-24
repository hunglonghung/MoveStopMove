using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinState : IState<Character>
{

    public void OnEnter(Character t)
    {
        t.ChangeAnim("dance");
    }

    public void OnExecute(Character t)
    {

    }

    public void OnExit(Character t)
    {

    }

}
