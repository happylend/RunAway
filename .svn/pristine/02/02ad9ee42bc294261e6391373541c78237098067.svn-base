using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMapNum : MonoBehaviour
{
    [Header("每一个图标对应一个关卡，MapNum保存图标对应关卡数")]
    public int Map_Num;
    public static int num;
    // Start is called before the first frame update
    void Start()
    {
        num = Map_Num;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnCreteClick()
    {
        music.PlaySound("点击");
        MapNum.Start_Num = Map_Num;
        MenuControl.key = Map_Num;

        Debug.Log("MapNum.Start_Num: " + MapNum.Start_Num);
    }
}
