using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    //默认
    Player_None,
    //移动
    Player_Move,
    //胜利
    Player_Win,
}

public class Player_Controller : FSMController<PlayerState>
{

    
    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }

    public CharacterController characterController { get; private set; }

    public Player_Model model{ get; private set; }

public Transform MovePoint;
    /// <summary>
    /// 状态开始的嘶吼
    /// </summary>
    private void Start()
    {
        MovePoint.parent = null;
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        
        model = GameObject.Find("Model").GetComponent<Player_Model>();
        model.Init(this);

        //characterController = GetComponent<CharacterController>();
        //默认是移动状态
        ChangeState<Player_Move>(PlayerState.Player_Move);
        Debug.Log(input.Horizontal);
    }
}
