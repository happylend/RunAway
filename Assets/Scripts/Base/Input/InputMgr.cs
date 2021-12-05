using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.Input类
/// 2.事件中心模块
/// 3.公共Mono模块的使用
/// </summary>
public class InputMgr :BaseManager<InputMgr>
{

    public static bool isStart = true;

    public Camera mainCam;
    /// <summary>
    /// 构造函数中 添加Update监听
    /// </summary>
    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    /// <summary>
    /// 是否开启或关闭 开启为3D 关闭为2D
    /// </summary>
    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    /// <summary>
    /// 检测按键抬起和按下 分发事件
    /// </summary>
    /// <param name="key"></param>
    private void CheckKeyCode(string key)
    {
        /*
        if(key.Contains("Horizontal") || key.Contains("Vertical"))
        {
            if (Mathf.Abs(Input.GetAxisRaw(key)) != 0)
                //事件中心模块 分发按键事件
                EventCenter.GetInstance().EventTrigger("Keydown", key);
        } 
        */
        if(key.Contains("W"))
        {
            if (Mathf.Abs(Input.GetAxisRaw(key)) != 0)
                EventCenter.GetInstance().EventTrigger("Keydown", key);
        }
    }
    private void CheckKeyCodeUp(KeyCode key)
    {
        if(Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("Keyup", key);
        }
    }


    /// <summary>
    /// Update监听
    /// </summary>
    private void MyUpdate()
    {
        //3D模式
        if(isStart)
        {
            CheckKeyCode("Horizontal");
            CheckKeyCode("Vertical");
            CheckKeyCodeUp(KeyCode.W);
            CheckKeyCodeUp(KeyCode.A);
            CheckKeyCodeUp(KeyCode.S);
            CheckKeyCodeUp(KeyCode.D);
            CheckKeyCodeUp(KeyCode.UpArrow);
            CheckKeyCodeUp(KeyCode.DownArrow);
            CheckKeyCodeUp(KeyCode.LeftArrow);
            CheckKeyCodeUp(KeyCode.RightArrow);

            //CheckKeyCode("")
            //CheckKeyCode("KeyCode.Space");
        }
        //2D模式
        else
        {
            return;
        }

    }

}
