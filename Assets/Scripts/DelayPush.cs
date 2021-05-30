using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 胜利时销毁场景
/// </summary>
public class DelayPush : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Win = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //当物体激活时，会进入的生命周期函数
    private void OnEnable()
    {
        //测试
        Invoke("Push", 1);
        if(Win == true)
        {
            Push();
        }
    }
    void Push()
    {
        //将物体压入缓存池
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
}
