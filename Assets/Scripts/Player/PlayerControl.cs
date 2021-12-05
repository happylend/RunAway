using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update


    public static int Ignorelayer = ~(1 << 30|1 << 27);//忽视藤蔓
    public static int IgnoreAirWall = ~(1 << 27);

    [Header("-----移动间隔-----")]
    public float timer = 0.1f;
    [Header("-----移动速度-----")]
    public static float speed = 1000; 

    private float T;
    public static bool getKey = false;
    public static GameObject people, box;

    public static float WallCheck = 0.7f;

    static int key = 1;

    public static bool Gress_Can_Move = false;
    private static Vector3 change;//箱子移动量
    private static Vector3 Movechange;//人物移动量

    private RaycastHit Icehit;
    public static TreasureBox treasure;

    public static Dir direction;
    public static Dir BoxDir;


    public static bool CanMove;
    public static Vector3 DIR;
    public enum Dir
    {
        idle,
        left,
        right,
        forward,
        back
    }

    void Start()
    {
       
        T = timer;
        //开启输入检测
        InputMgr.GetInstance().StartOrEndCheck(true);

        //添加事件中心检测
        EventCenter.GetInstance().AddEventListener("Keydown", CheckInputDown);
        EventCenter.GetInstance().AddEventListener("StateKeydown", CheckKey);
        people = this.gameObject;
        
        Movechange = this.transform.position;
        Movechange.y = 1;
        this.transform.position = Movechange;
    }



    private void Update()
    {


        if (getKey == false)
        {
            T -= Time.deltaTime;
            
        }

        if (T <= 0)
        {
            getKey = true;
            T = timer;
        }

        GerDirection();
        PlayerMoveRot();
        /*
        if (getKey == false && FallCheck.flag == false)
        {
            //people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
            //people.transform.position = Movechange;
        }
        */
    }
    

    public static void CheckKey(object key)
    {
        if(getKey)
        {
            
            switch(key)
            {
                case KeyCode.W:
                    DIR = Vector3.forward;
                        break;
                case KeyCode.A:
                    DIR = Vector3.left;
                    break;
                case KeyCode.S:
                    DIR = Vector3.back;
                    break;
                case KeyCode.D:
                    DIR = Vector3.right;
                    break;
            }

        }
    }

    public static void Move(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(people.transform.position, dir, out hit, WallCheck, IgnoreAirWall))
        {
            //人物移动
            box = hit.transform.gameObject;
            if (hit.transform.tag == "Treasure")
            {
                treasure = new TreasureBox(box);
                CanMove = treasure.CanMove(dir);
            }

        }
    }


    public static void CheckInputDown(object key)
    {
        if (getKey)
        {
            RaycastHit hit;
            string code = (string)key;

            switch (code)
            {
                case "Horizontal":
                    //检测是否有碰撞
                    Debug.DrawRay(people.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), Color.red, WallCheck);
                    if (Physics.Raycast(people.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), out hit, WallCheck, IgnoreAirWall))//发生碰撞Ignorelayer
                    {
                        //人物最后的按键
                        Restart.PeoplePos = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        box = hit.transform.gameObject;
                        if (hit.transform.tag == "Box")//碰撞到箱子
                        {
                            TreasureCheck(hit.transform.gameObject);
                           
                        }
                        else if (hit.transform.tag == "Fire")//推火焰
                        {
                            FireCheck(hit.transform.gameObject);

                        }
                        else if (hit.transform.tag == "Stone")//推石头
                        {
                            BoxCheck(hit.transform.gameObject);
                        }
                        else if (hit.transform.tag == "Blow")//推吹风机
                        {
                            BoxCheck(hit.transform.gameObject);
         
                        }
                        else if (hit.transform.tag == "Grass")//推草堆
                        {
                            GrassCheck(hit.transform.gameObject);
   
                        }
                        else if(hit.transform.tag == "IceBlock")//推冰块
                        {
                            BoxCheck(hit.transform.gameObject);
 
                        }
                        else//拓展
                            return;
                    }
                    else//没有碰撞
                    {
                        if (Input.GetAxisRaw("Horizontal") == 1f || Input.GetAxisRaw("Horizontal") == -1f)
                        {
                            Movechange = people.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                            people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
                            EventCenter.GetInstance().EventTrigger("PlayerWalk", key);
                            getKey = false;
                        }

                    }
                    break;

                case "Vertical":
                    if (Physics.Raycast(people.transform.position, new Vector3(0f, 0f, Input.GetAxisRaw("Vertical")), out hit, WallCheck, IgnoreAirWall))//发生碰撞Ignorelayer
                    {
                        //人物最后的按键
                        Restart.PeoplePos = new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));

                        box = hit.transform.gameObject;
                        if (hit.transform.tag == "Box")
                        {
                            TreasureCheck(hit.transform.gameObject);
                        }
                        else if (hit.transform.tag == "Fire")
                        {
                            FireCheck(hit.transform.gameObject);
                        }
                        else if (hit.transform.tag == "Stone")
                        {
                            BoxCheck(hit.transform.gameObject);
                        }
                        else if (hit.transform.tag == "Blow")
                        {
                            BoxCheck(hit.transform.gameObject);
                        }
                        else if (hit.transform.tag == "Grass")
                        {
                            GrassCheck(hit.transform.gameObject);
                        }
                        else if (hit.transform.tag == "IceBlock")//推冰块
                        {
                            BoxCheck(hit.transform.gameObject);
                        }
                        else//拓展
                            return;
                    }
                    else//没有碰撞
                    {
                        if(Input.GetAxisRaw("Vertical") == 1f || Input.GetAxisRaw("Vertical") == -1f)
                        {
                            Movechange = people.transform.position + new Vector3(0f, 0f, Input.GetAxisRaw("Vertical"));
                            people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
                            EventCenter.GetInstance().EventTrigger("PlayerWalk", key);
                            getKey = false;
                        }
                        
                    }
                    break;
            }
        }
    }

    public static void TreasureCheck(GameObject hit)
    {
        RaycastHit boxhit;
        if (Physics.Raycast(hit.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")), out boxhit, WallCheck, IgnoreAirWall))
        {
            change = new Vector3(0, 0, 0);
        }
        else
        {
            Movechange = people.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            change = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            //触发声效
            EventCenter.GetInstance().EventTrigger("BoxMoveSound", key);
        }
        people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
        hit.transform.position = hit.transform.position + change;
        getKey = false;
    }

    public static void BoxCheck(GameObject hit)
    {
        RaycastHit boxhit;
        if (Physics.Raycast(hit.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")), out boxhit, WallCheck))
        {
            change = new Vector3(0, 0, 0);
        }
        else
        {
            Movechange = people.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            change = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            //触发声效
            EventCenter.GetInstance().EventTrigger("BoxMoveSound",key);
        }
        people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
        hit.transform.position = hit.transform.position + change;
        getKey = false;
    }

    public static void FireCheck(GameObject hit)
    {
        RaycastHit firehit;
        if (Physics.Raycast(hit.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")), out firehit, WallCheck, Ignorelayer))
        {
            change = new Vector3(0, 0, 0);
        }
        else
        {
            Movechange = people.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            change =  new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            //触发声效
            EventCenter.GetInstance().EventTrigger("BoxMoveSound", key);
        }
        people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
        hit.transform.position = hit.transform.position + change;
        getKey = false;
    }

    public static void GrassCheck(GameObject hit)
    {
        RaycastHit gresshit;
        if (Physics.Raycast(hit.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")), out gresshit, WallCheck))
        {
            Movechange = people.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            change = new Vector3(0, 0, 0);
        }
        else
        {
            Movechange = people.transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            change = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            //触发声效
            EventCenter.GetInstance().EventTrigger("BoxMoveSound", key);
        }
        people.transform.position = Vector3.MoveTowards(people.transform.position, Movechange, speed * Time.deltaTime);
        hit.transform.position = hit.transform.position + change;
        getKey = false;
    }

    //获取移动方向
    void GerDirection()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            direction = Dir.right;
            BoxDir = Dir.right;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            direction = Dir.left;
            BoxDir = Dir.left;
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            direction = Dir.forward;
            BoxDir = Dir.forward;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            direction = Dir.back;
            BoxDir = Dir.back;
        }
    }



    void PlayerMoveRot()
    {
        switch(direction.ToString())
        {
            case "right":
                transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            case "left":
                transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                break;
            case "forward":
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
            case "back":
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                break;
        }

    }


}