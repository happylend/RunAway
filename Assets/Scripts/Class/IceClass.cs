using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceClass : MonoBehaviour
{
    private Vector3 _Pos;

    public Vector3 Pos
    {
        get 
        {
               _Pos = this.transform.position;
                return _Pos;
        }
    }
    public enum _icestatue
    {
        idle,
        changed
    }
    public static _icestatue IceStatue = _icestatue.idle;


    /// <summary>
    /// 滑冰
    /// </summary>
    /// <param name="collision"></param>
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" || collision.transform.tag == "IceBlock"
            || collision.transform.tag == "Stone "|| collision.transform.tag =="Box")
        {
            //关闭输入
            InputMgr.GetInstance().StartOrEndCheck(false);
            //
        }
    }
    */
}
