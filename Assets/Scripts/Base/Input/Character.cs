using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character 
{
    public IChState _state;

    public void SetCHState(IChState newState)
    {
        _state = newState;
    }
}
