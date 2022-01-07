using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ChangeWorld : StateBase<PlayerState>
{
    public Player_Controller Player;

    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        //Player是Player_Controller的基类
        Player = controller as Player_Controller;
    }
    public override void OnEnter()
    {
        Map.MapComplete = false;
        //检测世界转换
        EventCenter.GetInstance().EventTrigger("ChangeWorld", MapNum.Start_Num);
    }

    public override void OnExit(){
    
    }
    public override void OnUpdate(){
        if (Map.MapComplete)  
        {
            if (Player.input.Horizontal == 0 && Player.input.Vertical == 0)
                Player.ChangeState<Player_Move>(PlayerState.Player_Move);
        }
    }
}
