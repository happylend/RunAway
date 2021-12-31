using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Dir
{
    idle,
    forward,
    back,
    left,
    right
}

public class Player_Move : StateBase<PlayerState>
{
    Player_Controller Player;

  

    private float moveSpeed = 4f;
    private GameObject _Box;
    private GameObject Box { get => _Box; set => _Box = value; }

    private int BoxLayer = Player_Controller.Ignorelayer;

    private float CheckLen = 1f;

    //private bool canMove = true;
    
    public static Dir dir;
    public static bool StopInput;

    
    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        //Player是Player_Controller的基类
        Player = controller as Player_Controller;
        if (Box is null)
        {
            Box = new GameObject();
            Box.name = "BoxName";
        }

    }

    // Start is called before the first frame update
    public override void OnEnter() 
    {
        Player_Controller.CanFall = Player_Controller.CanMove = Player_Controller.CanChangeWorld = true;
        
        StopInput = false;

    }

    public override void OnExit()
    {
        StopInput = true;
        Player_Controller.CanMove = false;
    }

    public override void OnUpdate()
    {

        if (Player_Controller.Win)
        {
            Debug.Log("胜利了！");
            GameObject.Destroy(Player.MovePoint.gameObject);
            GameObject.Destroy(GameObject.Find("BoxName"));
            StopInput = true;
            if (Vector3.Distance(Player.transform.position, Player.MovePoint.position) < 0.2f)
            {
                Player.ChangeState<Player_Win>(PlayerState.Player_Win);
            }
        }
        //检测是否能飞走
        Blow();


        //检测切换世界
        if (Player_Controller.CanChangeWorld) ChangeWorld();

        //检测是否下落
        if (Player_Controller.CanFall) Fall();
        
        //移动
        if (Player_Controller.CanMove) Move();          




        //如果胜利


    }
    /// <summary>
    /// 判断r人物是否可以移动
    /// </summary>
    /// <param name="h">水平移动</param>
    /// <param name="v">垂直移动</param>
    /// <returns></returns>
    private bool CanMove(float h, float v, Player_Controller Player)
    {
        //Debug.Log("h是" + h + "v是" + v);
        //RaycastHit Barhit;


        //检测到前面有物品
        if(Physics.Raycast(Player.transform.position, new Vector3(h, 0, v),out RaycastHit boxhit, 1f,Player_Controller.Ignorelayer))
        {
            //如果不是箱子
            if(boxhit.transform.tag != "Box")
            {
                Debug.Log("不是箱子");
                return false;
            }

            //如果是箱子
            else
            {
                
                //获得箱子
                Box = boxhit.transform.gameObject;
                if(Box.TryGetComponent<Treasure>(out Treasure treasure)) BoxLayer = Player_Controller.IgnoreAirWall;
                else BoxLayer = Player_Controller.Ignorelayer;

                //箱子前面没有东西
                if (!Physics.Raycast(Box.transform.position, new Vector3(h, 0, v), 1f, BoxLayer))
                {
                    return true;
                }
                return false;
            }
        }
        return true; 
    }
    
    /// <summary>
    /// 人物移动函数
    /// </summary>
    private void Move()
    {
        //Debug.Log("开始调用移动");
        if (CanMove(Player.input.Horizontal, Player.input.Vertical, Player))
        {
            float h, v;
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, Player.MovePoint.position, moveSpeed * Time.deltaTime);

            if (!StopInput)
            {
                h = Player.input.Horizontal;
                v = Player.input.Vertical;
            }
            else h = v = 0;

            if(Vector3.Distance(Player.transform.position, Player.MovePoint.position)<=.006f)
            {
                if (Mathf.Abs(h) == 1f)
                {
                    if (h > 0) dir = Dir.right;
                    else dir = Dir.left;
                    //转向
                    Player.transform.eulerAngles = new Vector3(0, h * 90f, 0);
                    //移动
                    Player.MovePoint.position += new Vector3(h, 0f, 0f);
                    Player.MovePoint.position = Player_Controller.RoundV(Player.MovePoint.position);


                    //同步模型动画
                    //Player.model.UpdateMovePar(h, v);
                }
                if (Mathf.Abs(v) == 1f)
                {
                    //转向
                    if (v > 0)
                    {
                        dir = Dir.forward;
                        Player.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        dir = Dir.back;
                        Player.transform.eulerAngles = new Vector3(0, 180, 0);
                    }          
                    //移动
                    Player.MovePoint.position += new Vector3(0f, 0f, v);
                    Player.MovePoint.position = Player_Controller.RoundV(Player.MovePoint.position);

                    //同步模型动画
                    // Player.model.UpdateMovePar(h, v);
                }
                //检测胜利
                if (Treasure.Iswin)
                {
                    if (Player.transform.position == Player.MovePoint.position)
                    {
                        Player_Controller.Win = true;
                    }
                }
            }
        }
        
    }
    /// <summary>
    /// 人物下落
    /// </summary>
   
    /// <summary>
    /// 下落监测
    /// </summary>
    /// <returns></returns>
    private bool CanFall()
    {
        if (!Physics.Raycast(Player.transform.position, new Vector3(0, -1f, 0), CheckLen, Player_Controller.RestartLayer)) return true;
        else return false;
    }
    private void Fall()
    {
        if (CanFall())
        {
            StopInput = true;

            if (Player.transform.position == Player.MovePoint.position)
            {
                GameObject.Destroy(GameObject.FindWithTag("Point"));
                Player.ChangeState<Player_Fall>(PlayerState.Player_Fall);

            } 
            
            
        }
    }

    /// <summary>
    /// 检测转换世界
    /// </summary>
    /// <returns></returns>
    private bool CanChangeWorld()
    {
        RaycastHit raycast;
        if (Physics.Raycast(Player.transform.position, Vector3.down, out raycast, CheckLen))
        {
            if (raycast.transform.tag == "ChangeWorld") return true;
            else return false;
        }
        else return false;
    }
    /// <summary>
    /// 转换世界
    /// </summary>
    private void ChangeWorld()
    {
        if(CanChangeWorld())
        {
            Player_Controller.CanFall  = Player_Controller.CanBlow = false;
            if (Player.transform.position == Player.MovePoint.position) 
                Player.ChangeState<Player_ChangeWorld>(PlayerState.Player_ChangeWorld);
        }
    }


    private void Blow()
    {    
        if(Player_Controller.CanBlow)
        {
            Player_Controller.CanFall = Player_Controller.CanChangeWorld =  false;
            StopInput = true;
            if (Vector3.Distance(Player.transform.position,Player.MovePoint.position)<0.15f)
            {
                Player.transform.position = Player.MovePoint.position;
                Player_Controller.CanMove = false;
                Debug.Log("进入吹风状态");
                Player.ChangeState<Player_Blow>(PlayerState.Player_Blow);
            }
        }





        

    }

}



