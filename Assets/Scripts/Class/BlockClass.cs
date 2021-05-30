using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClass : MonoBehaviour
{
    //坐标
    private Vector3 _Pos;
    //改变状态
    public enum _blockstatue
    {
        idle,
        changed,
    }
    public static _blockstatue BlockStatue = _blockstatue.idle;

    public Vector3 Pos
    {
        get
        {
            _Pos = this.transform.position;
            return _Pos;
        }
    }
    private void Start()
    {
        BlockStatue = _blockstatue.idle;
    }
    // Start is called before the first frame update


}
