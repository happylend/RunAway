using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallCheck : MonoBehaviour
{
    public static int RestartLayer = ~(1 << 29| 1<<28);//忽略重启区域
    private Vector3 StartPos;
    public float checkWall = 1f;
    public static bool flag = false;
    public static bool CanFall = true;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanFall)
        {
            flag = Fall(Vector3.down);
            CheckFall();
        }

        

    }
    //下落检测
    bool Fall(Vector3 pos)
    {
        if (!Physics.Raycast(transform.position, new Vector3(0, -1f, 0), checkWall, RestartLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void CheckFall()
    {
        if (flag)
        {                 
            GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePositionY;
            //EventCenter.GetInstance().RomoveEventListener("Keydown", PlayerControl.CheckInputDown);//移除输入监听
            Debug.Log("定住人物");
        }
        else
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
