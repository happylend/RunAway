using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Push : StateBase<PlayerState>
{
    public Player_Controller player;
    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        player = controller as Player_Controller;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        
    }

    
}
