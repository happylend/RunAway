using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMap : MonoBehaviour
{
    private int key = 2;//播放胜利音效专用变量
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "Box")
        {
            InputMgr.GetInstance().StartOrEndCheck(false);//关闭输入
            EventCenter.GetInstance().RomoveEventListener("Keydown", PlayerControl.CheckInputDown);//移除输入监听
            MusicMgr.GetInstance().PauseBKMusic();//停止音效
            BoxMoveRot(collision.transform.gameObject);//宝箱朝向玩家
            EventCenter.GetInstance().EventTrigger("Win", key);//胜利音效
            OpenBox.Open = true;//宝箱打开动画
            MapControl.Success = true;//弹出菜单

        }
    }

    public void BoxMoveRot(GameObject hit)
    {
        switch (PlayerControl.BoxDir.ToString())
        {
            case "right":
                hit.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                break;
            case "left":
                hit.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                break;
            case "forward":
                hit.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                break;
            case "back":
                hit.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                break;
        }
    }
}
