using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        EventCenter.GetInstance().AddEventListener("InitMap", PlayBK);
        EventCenter.GetInstance().AddEventListener("ChangeMap", PlayBK);
        EventCenter.GetInstance().AddEventListener("RestartGame", ResetBK);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayBK(object key)
    {
        
        MusicMgr.GetInstance().PlayBkMusic("GameSceneBGM");
       
    }
    public static void ResetBK(object key)
    {
        MusicMgr.GetInstance().StopBKMusic();
        MusicMgr.GetInstance().PlayBkMusic("GameSceneBGM");
    }
}
