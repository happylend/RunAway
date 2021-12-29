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
        //Player_Controller.ChangeMapActive = true;
        //检测世界转换
        EventCenter.GetInstance().EventTrigger("ChangeWorld", MapNum.Start_Num);
        if(Map.MapComplete)
            Player.ChangeState<Player_Move>(PlayerState.Player_Move);


    }

    public override void OnExit(){ }
    public override void OnUpdate(){ }
}
