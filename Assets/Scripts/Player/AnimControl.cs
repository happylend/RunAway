using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    [HideInInspector]
    public static Animator anim;  // animation controller 动画文件


    void Start()
    {
        anim = GetComponent<Animator>(); //初始化动画
    }

    void Update()
    {
    }
    public static void Init()
    {
        anim.SetBool("IsWin", false);
        anim.SetBool("IsPush", false);
        anim.SetBool("IsDrop", false);
    }
    public static void Drop()
    {
        anim.SetBool("IsWin", false);
        anim.SetBool("IsPush", false);
        anim.SetBool("IsDrop", false);
        anim.SetBool("IsDrop", true);
    }
    public static void Push()
    {
        anim.SetBool("IsWin", false);
        anim.SetBool("IsPush", false);
        anim.SetBool("IsDrop", false);
        anim.SetBool("IsPush", true);
    }
    public static void Win()
    {
        anim.SetBool("IsWin", false);
        anim.SetBool("IsPush", false);
        anim.SetBool("IsDrop", false);
        anim.SetBool("IsWin", true);
    }
}
