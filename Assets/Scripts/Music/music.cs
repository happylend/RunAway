using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    //存储加载完的音效
    
    public static  AudioSource source;
    public bool over = true;
    int num = 5;
    //mmint num;
    // Start is called before the first frame update
    void Awake()
    {

        //空监听按键
        EventCenter.GetInstance().AddEventListener("Keydown", nullEvent);

        //监听人行走
        EventCenter.GetInstance().AddEventListener("PlayerWalk", WalkMusic);

        //监听胜利
        EventCenter.GetInstance().AddEventListener("Win", WinSound);

        //监听落水
        EventCenter.GetInstance().AddEventListener("DropWater", Dropsound);

        //监听箱子被推动
        EventCenter.GetInstance().AddEventListener("BoxMoveSound", BoxSound);
        //EventCenter.GetInstance().AddEventListener("Keyup", StopMusic);
    }


    /// <summary>
    /// 按键监听的空事件
    /// </summary>
    /// <param name="key"></param>
    void nullEvent(object key)
    {
        return;
    }

    /// <summary>
    /// 胜利事件
    /// </summary>
    /// <param name="key"></param>
    void WinSound(object key)
    {
        PlaySound("胜利");
    }

    /// <summary>
    /// 掉水音效
    /// </summary>
    /// <param name="key"></param>
    void Dropsound(object key)
    {
        Debug.Log("下水");
        PlaySound("落水");
    }

    /// <summary>
    /// 箱子摩擦的声音
    /// </summary>
    /// <param name="key"></param>
    void BoxSound(object key)
    {
        Debug.Log("摩擦");
        PlaySound("物体和地面摩擦");
    }

    /// <summary>
    /// 人行走的声音
    /// </summary>
    /// <param name="key"></param>
    // Update is called once per frame
    void WalkMusic(object key)
    {
        //走在草地关
        if (num < 6)
        {
            PlaySound("草地行走");
        }  
        //在冰雪关         
        else
        {
            PlaySound("雪地行走");
        }           
    }


    /// <summary>
    /// 播放音效接口
    /// </summary>
    /// <param name="name"></param>
    public static void PlaySound(string name)
    {

        GameObject obj = GameObject.Find("Sound");
        //第一次进入
        if (obj == null)
        {
            //播放音效
            MusicMgr.GetInstance().PlaySound(name, false, (sr) => {
                source = sr;
                source.volume = 0.3f;
            });
        }
        //Sound已被加载
        else
        {
            //如果里面有音乐
            if(obj.GetComponent<AudioSource>() != null)
            {
                //暂停音乐
                MusicMgr.GetInstance().StopSound(source);
                //播放音乐
                MusicMgr.GetInstance().PlaySound(name, false, (sr) => {
                    source = sr;
                    source.volume = 0.3f;
                });
                return;
            }
            //没有音乐
            else
            {
                //播放音乐
                MusicMgr.GetInstance().PlaySound(name, false, (sr) => {
                    source = sr;
                    source.volume = 0.3f;
                });
                return;
            }
        }     

    }


}
