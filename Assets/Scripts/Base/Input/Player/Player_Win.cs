using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Win : StateBase<PlayerState>
{
    public Player_Controller Player;

    public GameObject Box { get; set; }


    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
    }

    // Start is called before the first frame update
    public override void OnEnter()
    {
        //播放动画
        //生成胜利菜单
        MusicMgr.GetInstance().PauseBKMusic();//停止音效
        EventCenter.GetInstance().EventTrigger("Win", 2);//胜利音效
        OpenBox.Open = true;//宝箱打开动画
        MapControl.Success = true;//弹出菜单


    }

    public override void OnExit()
    {
        Player_Controller.Win = false;
        Debug.Log("胜利状态结束了");
    }

    public override void OnUpdate()
    {
        //
    }


}
