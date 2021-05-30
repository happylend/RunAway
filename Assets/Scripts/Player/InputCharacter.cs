using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputCharacter : MonoBehaviour//已废弃
{
    // Start is called before the first frame update
    public static float moveSpeed = 3f;

    public static LayerMask WhereStopMove = 1 << 11;
    public static LayerMask box = 1 << 10;
    public static int Ignorelayer = ~(1 << 30);

    public float gravity = -9.8f;
    public float timer = 0.3f;

    private float T;
    public static bool getKey = true;
    public static GameObject people;
    public static float WallCheck = 1f;

    public static Vector3 change;
    //检测人物方向
    public enum PersonDt
    {
        idle,
        Forward,
        Back,
        Left,
        Rigt
    };
    public static PersonDt currentPdt;
    void Awake()
    {
        T = timer;
       
        //开启输入检测
        InputMgr.GetInstance().StartOrEndCheck(true);
  
        //添加事件中心检测
        //EventCenter.GetInstance().AddEventListener("Keydown", CheckInputDown);
        //EventCenter.GetInstance().RomoveEventListener("Keydown", CheckInputDown);
        people = this.gameObject;

    }

    private void FixedUpdate()
    {
        
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
                    if (Physics.Raycast(InputCharacter.people.transform.position, new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), out hit, WallCheck, Ignorelayer))//发生碰撞
                    {
                        Debug.Log("左右发生碰撞！");
                        //右
                        if (Input.GetAxisRaw("Horizontal") == 1f)
                        {
                            if (hit.transform.tag == "Box")//碰撞到箱子
                            {
                                Debug.Log("右侧碰到箱子！");
                                RaycastHit boxhit;
                                if (Physics.Raycast(hit.transform.position, new Vector3(1, 0f, 0f), out boxhit, WallCheck))
                                {
                                    change = new Vector3(0, 0, 0);
                                    return;
                                }
                                else
                                {
                                    Debug.Log("右移！");
                                    InputCharacter.people.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1.5f, 0f, 0f);
                                    change = new Vector3(1.5f, 0, 0);
                                    currentPdt = PersonDt.Rigt;                                                                     
                                }
                                hit.transform.position = hit.transform.position + change;
                                getKey = false;
                            }
                            else if (hit.transform.tag == "Fire")
                            {
                                RaycastHit firehit;
                                if (Physics.Raycast(hit.transform.position, new Vector3(1, 0f, 0f), out firehit, WallCheck, Ignorelayer))
                                {
                                    change = new Vector3(0, 0, 0);
                                    return;
                                }
                                else
                                {
                                    Debug.Log("右移！");
                                    InputCharacter.people.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1.5f, 0f, 0f);
                                    change = new Vector3(1.5f, 0, 0);
                                }
                                hit.transform.position = hit.transform.position + change;
                                getKey = false;

                            }
                            else//拓展
                                return;
                        }

                        //左
                        else if (Input.GetAxisRaw("Horizontal") == -1f)
                        {
                            if (hit.transform.tag == "Box")//碰撞到箱子
                            {
                                Debug.Log("左侧碰到箱子！");
                                RaycastHit boxhit;
                                if (Physics.Raycast(hit.transform.position, new Vector3(-1, 0f, 0f), out boxhit, WallCheck))
                                {
                                    change = new Vector3(0, 0, 0);
                                    return;
                                }
                                else
                                {
                                    Debug.Log("左移！");
                                    InputCharacter.people.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1.5f, 0f, 0f);
                                    change = new Vector3(-1.5f, 0, 0);
                                    currentPdt = PersonDt.Left;
                                }
                                hit.transform.position = hit.transform.position + change;
                                getKey = false;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else//没有碰撞
                    {
                        if (Input.GetAxisRaw("Horizontal") == 1f)
                        {
                            InputCharacter.people.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1.5f, 0f, 0f);
                            getKey = false;
                        }
                        else if (Input.GetAxisRaw("Horizontal") == -1f)
                        {
                            InputCharacter.people.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1.5f, 0f, 0f);
                            getKey = false;
                        }
                        else
                            return;
                    }
                    break;


                case "Vertical":
                    if (Physics.Raycast(InputCharacter.people.transform.position, new Vector3(0f, 0f, Input.GetAxisRaw("Vertical")), out hit, WallCheck, Ignorelayer))//发生碰撞
                    {
                        Debug.Log("前后发生碰撞！");
                        //前
                        if (Input.GetAxisRaw("Vertical") == 1f)
                        {
                            if (hit.transform.tag == "Box")
                            {
                                Debug.Log("前方碰到箱子！");
                                RaycastHit boxhit;
                                if (Physics.Raycast(hit.transform.position, new Vector3(0, 0f, 1f), out boxhit, WallCheck))
                                {
                                    change = new Vector3(0, 0, 0);
                                    return;
                                }
                                else
                                {
                                    Debug.Log("前进！");
                                    InputCharacter.people.transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 1.5f);
                                    change = new Vector3(0, 0, 1.5f);
                                    currentPdt = PersonDt.Forward;
                                }
                                hit.transform.position = hit.transform.position + change;
                                getKey = false;
                            }
                            else//拓展
                                return;
                        }

                        //后
                        else if (Input.GetAxisRaw("Vertical") == -1f)
                        {
                            if (hit.transform.tag == "Box")
                            {
                                Debug.Log("后方碰到箱子！");
                                RaycastHit boxhit;
                                if (Physics.Raycast(hit.transform.position, new Vector3(0, 0f, -1f), out boxhit, WallCheck))
                                {
                                    change = new Vector3(0, 0, 0);
                                    return;
                                }
                                else
                                {
                                    Debug.Log("后退！");
                                    InputCharacter.people.transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 1.5f);
                                    change = new Vector3(0, 0, -1.5f);
                                }
                                hit.transform.position = hit.transform.position + change;
                                getKey = false;
                            }
                            else//拓展
                                return;

                        }
                    }
                    else//没有碰撞
                    {
                        if (Input.GetAxisRaw("Vertical") == 1f)
                        {
                            InputCharacter.people.transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 1.5f);
                            getKey = false;
                        }
                        else if (Input.GetAxisRaw("Vertical") == -1f)
                        {
                            InputCharacter.people.transform.position += new Vector3(0f, 0f, Input.GetAxisRaw("Vertical") * 1.5f);
                            getKey = false;
                        }
                        else
                            return;
                    }

                    break;
            }

        }
    }

    void BoxCheck(GameObject hit)
    {
        RaycastHit boxhit;
        if (Physics.Raycast(hit.transform.position, new Vector3(1, 0f, 0f), out boxhit, WallCheck))
        {
            change = new Vector3(0, 0, 0);
            return;
        }
        else
        {
            Debug.Log("右移！");
            InputCharacter.people.transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1.5f, 0f, 0f);
            change = new Vector3(1.5f, 0, 0);
        }
        hit.transform.position = hit.transform.position + change;
        getKey = false;
    }


}
