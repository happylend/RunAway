using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IChState
{
    Character character;
    StateMachine stateMachine;

    public IChState(Character character, StateMachine state)
    {
        this.character = character;
        this.stateMachine = state;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }

    public virtual void Update() { }
    public virtual void HandleInput() { }

    public virtual void PhysicsUpdate() { }
    

}
