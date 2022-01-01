using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input 
{


    public float Horizontal { get => Input.GetAxisRaw("Horizontal"); }
    public float Vertical { get => Input.GetAxisRaw("Vertical"); } 

  

    /// <summary>
    /// 判断是否长按按下按键
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
    /// <summary>
    /// 判断是否按下某键
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
}
