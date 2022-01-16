using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画层
/// </summary>
public class Player_Model : MonoBehaviour
{
    public Player_Controller player;
    private Animator animator;
    // Start is called before the first frame update
    public void Init(Player_Controller player)
    {
        this.player = player;
        //初始完成;

        if(TryGetComponent<Animator>(out Animator Ani))
        {
            Debug.Log(Ani);
            animator = Ani;
        }
    }

    /// <summary>
    /// 同步移动相关动画
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    public void UpdateMovePar(float h, float v)
    {
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
    }
}
