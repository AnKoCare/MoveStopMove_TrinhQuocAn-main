using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulti : IState<Character>
{
    public void OnEnter(Character t)
    {
        t.OnUltiEnter();
    }

    public void OnExecute(Character t)
    { 
        t.OnUltiExecute();
    }

    public void OnExit(Character t)
    {
        t.OnUltiExit();
    }
}
