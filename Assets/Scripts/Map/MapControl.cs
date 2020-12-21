using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Setting;
    public GameObject SettingObj;
    public static int Restart_Num;//重置关卡
    public static bool Success;
    // Start is called before the first frame update
    void Start()
    {
        Restart_Num = MapNum.Start_Num;
        Menu.SetActive(false);
        Setting.SetActive(false);
        Success = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu.active == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                EventCenter.GetInstance().EventTrigger("RestartGame", Restart_Num);
            }
        }

        if (Success)
        {
            Menu.SetActive(true);
            Success = false;
        }
    }

    public void openSetting()
    {
        music.PlaySound("点击");
        SettingObj.SetActive(false);
        Setting.SetActive(true);
    }
}
