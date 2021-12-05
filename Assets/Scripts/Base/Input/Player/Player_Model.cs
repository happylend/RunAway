using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画层
/// </summary>
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    // Start is called before the first frame update
    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 同步移动相关动画
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void UpdateMovePar(float h, float v)
    {
        animator.SetFloat("Horizontal",h);
        animator.SetFloat("Vertical", v);
    }
}
