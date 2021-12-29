using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fall : StateBase<PlayerState>
{
    public Player_Controller Player;
    public float CheckLen = 1f;

    // Start is called before the first frame update
    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        //Player是Player_Controller的基类
        Player = controller as Player_Controller;
    }
    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Point"));
    }

    public override void OnUpdate()
    {

        Player.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
        //掉落动画

        //重置关卡

        //回到移动状态
        Player.ChangeState<Player_Move>(PlayerState.Player_Move);
    }

}
