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
    //下落
    Player_Fall,
    //切换世界
    Player_ChangeWorld,
    //吹风机
    Player_Blow

}

public class Player_Controller : FSMController<PlayerState>
{

    public static int Ignorelayer = ~(1 << 30 | 1 << 27);//忽视藤蔓
    public static int IgnoreAirWall = ~(1 << 27);
    public static int ignoreGrass = ~(1 << 30);
    public static int RestartLayer = ~(1 << 29 | 1 << 28);//忽略重启区域
    //public static bool ChangeMapActive = false;
    public static bool CanBlow, CanMove, CanChangeWorld, CanFall;

    public static Vector3 BlowDir;

    public Player_Input input { get;  set; }
    public new Player_Audio audio { get; private set; }

    public static bool Win;

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
        Win = false;

        CanMove = CanChangeWorld = CanFall = true;
        //characterController = GetComponent<CharacterController>();
        //默认是移动状态
        ChangeState<Player_Move>(PlayerState.Player_Move);
        //Debug.Log(input.Horizontal);
    }

    /// <summary>
    /// 数目的四舍五入
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static int Round(float num)
    {
        if (num > 0) return (int)(num + 0.5f);
        else return (int)(num - 0.5f);
    }

    public static Vector3 RoundV(Vector3 vector)
    {
        return new Vector3(Round(vector.x), Round(vector.y), Round(vector.z));
    }

    public Vector3 Tran()
    {
        return this.transform.position;
    }

}
