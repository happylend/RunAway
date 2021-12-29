using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 状态对象基类
/// 之后所有状态都继承这个类
/// Idle Walk 
/// </summary>
public abstract class StateBase<T> 
{
    //状态存在进入 更新 退出的枚举状态
    public T StateType;//这个表示状态类型

    /// <summary>
    /// 首次实例化时的初始化
    /// </summary>
    /// <param name="StateType"></param>
    public virtual void Init(FSMController<T> controller,T StateType)
    { 
        this.StateType = StateType; 
    }


    //进入
    public abstract void OnEnter();

    //更新
    public abstract void OnUpdate();

    //退出
    public abstract void OnExit();
}
