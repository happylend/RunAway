using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Win : StateBase<PlayerState>
{
    public Player_Controller Player;
    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
    }

    // Start is called before the first frame update
    public override void OnEnter()
    {
        //播放动画

        //生成胜利菜单
    }

    public override void OnExit()
    {
        Player.ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    public override void OnUpdate()
    {




        //
    }
}
