using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //显示面板
        UIManager.GetInstance().ShowPanel<LoginPanel>("LoginPanel", E_UI_layer.Bot, ShowPanelOver);
        //创建对象 让对象做什么
        //LoginPanel p = this.gamoObject.GetComponent<LoginPanel>();
        //让这个对象做什么
        //p.InitInfo

    }
    //面板创建后 做
    private void ShowPanelOver(LoginPanel panel)
    {
        panel.InitInfo();
        //延迟隐藏
        Invoke("DelayHide", 1);
    }

    private void DelayHide()
    {
        UIManager.GetInstance().HidePanel("LoginPanel");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
