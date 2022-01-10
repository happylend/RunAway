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
    Player_Blow,
    //滑冰
    Player_Skate,
    //推箱子
    Player_Push
}

public class Player_Controller : FSMController<PlayerState>
{

    public static int Ignorelayer = ~(1 << 30 | 1 << 27);//忽视藤蔓
    public static int IgnoreAirWall = ~(1 << 27);
    public static int IgnoreGrass = ~(1 << 30);
    public static int IgnoreIceAir = ~(1 << 11| 1 << 27);//忽略冰和空气墙
    public static int IgnoreIce = ~(1 << 11 );//忽略冰
    public static int RestartLayer = ~(1 << 29 | 1 << 28);//忽略重启区域
    public static int IceLayer = (1 << 11);//忽略冰


    //public static bool ChangeMapActive = false;
    public static bool CanBlow, CanMove, CanChangeWorld, CanFall, CanSkate;

    public static Vector3 BlowDir;

    public Player_Input input { get;  set; }
    public new Player_Audio audio { get; private set; }

    public static bool Win;
    public static bool Push;

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
        //默认是移动状态
        ChangeState<Player_Move>(PlayerState.Player_Move);

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

    /// <summary>
    /// 位置
    /// </summary>
    /// <returns></returns>
    public Vector3 Tran()
    {
        return this.transform.position;
    }

    /// <summary>
    /// 获得坐标
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Vector3 GetDir(Dir dir)
    {
        Vector3 vector;
        switch (dir)
        {
            case Dir.idle:
                vector = Vector3.zero;
                break;
            case Dir.forward:
                vector = Vector3.forward;
                break;
            case Dir.back:
                vector = Vector3.back;
                break;
            case Dir.left:
                vector = Vector3.left;
                break;
            case Dir.right:
                vector = Vector3.right;
                break;
            default:
                vector = Vector3.zero;
                break;
        }
        return vector;

    }

    
    

}
