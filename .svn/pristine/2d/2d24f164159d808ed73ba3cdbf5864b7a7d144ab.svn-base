using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理器
/// 1.管理所有显示的面板
/// 2.提供给外部 显示和隐藏等等接口
/// </summary>
public class LoginPanel : BasePanel
{

    protected override void Awake()
    {
        //一定不能少 因为需要执行父类的awake来初始化一些信息 比如找控件 加时间监听
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        //使用按钮
       // GetControl<Button>("btnStart").onClick.AddListener(ClickStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void InitInfo()
    {
        Debug.Log("初始化数据");
    }

    /// <summary>
    /// 显示面板的重载函数
    /// </summary>
    public override void ShowMe()
    {
        base.ShowMe();
        //显示面板时 想要执行的逻辑 这个函数 在UI管理器中 会自动帮我们调用
        //在下面处理一些自己的初始化逻辑
    }

    protected override void OnClick(string btnName)
    {
        switch(btnName)
        {
            case "btnStart":
                Debug.Log("btnStart被点击");
                break;
            case "btnQuit":
                Debug.Log("btnQuit被点击");
                break;
        }
    }

    protected override void OnValueChange(string toggleName, bool value)
    {
        //根据名字判断 到底是哪一个单选框或者更多选框变化了 当前状态都是传入的value
       
    }
    //点击开始按钮的处理
    public void ClickStart()
    {
        //UIManager.GetInstance().showPanel<LoadingPanel>("LoadingPanel");


    }
}
