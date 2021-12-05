using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Move : StateBase<PlayerState>
{
    public Player_Controller Player;
    private float moveSpeed = 3f;

    public override void Init(FSMController<PlayerState> controller, PlayerState StateType)
    {
        base.Init(controller, StateType);
        //Player是Player_Controller的基类
        Player = controller as Player_Controller;
     
    }

    // Start is called before the first frame update
    public override void OnEnter() { 
    
    }

    public override void OnExit(){}

    public override void OnUpdate()
    {
        Move();
        //如果胜利
    }
    /// <summary>
    /// 判断箱子是否可以移动
    /// </summary>
    /// <param name="h">水平移动</param>
    /// <param name="v">垂直移动</param>
    /// <returns></returns>
    private bool CanMove(float h, float v)
    {
        Debug.Log("h是" + h + "v是" + v);
        RaycastHit Barhit;

        //检测到前面有物品
        if(Physics.Raycast(Player.transform.position, new Vector3(h, 0, v),out RaycastHit boxhit, 1f))
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
                Debug.Log("前面有箱子");
                //箱子前面有东西
                if (Physics.Raycast(boxhit.transform.position, new Vector3(h, 0, v), out Barhit, 0.7f))   
                    return false;
                {
                    //进入推箱子的状态
                    boxhit.transform.position += new Vector3(h, 0, v);
                    return true;
                }
 
            }
        }
        //Debug.Log("可以通行");
        return true;
        
    }
    /// <summary>
    /// 移动函数
    /// </summary>
    public void Move()
    {
        if (CanMove(Player.input.Horizontal,Player.input.Vertical))
        {
            
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, Player.MovePoint.position, moveSpeed * Time.deltaTime);
            float h = Player.input.Horizontal;
            float v = Player.input.Vertical;
            
            if(Vector3.Distance(Player.transform.position, Player.MovePoint.position)<=.05f)
            {
                if (Mathf.Abs(h) == 1f)
                {
                    Player.MovePoint.position += new Vector3(h, 0f, 0f);
                    //同步模型动画
                    Player.model.UpdateMovePar(h, v);

                }
                if (Mathf.Abs(v) == 1f)
                {
                    Player.MovePoint.position += new Vector3(0f, 0f, v);
                    //同步模型动画
                    Player.model.UpdateMovePar(h, v);
                }
            }
        }
        
    }
}
