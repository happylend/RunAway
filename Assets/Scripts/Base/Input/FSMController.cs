using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 有限状态机控制器
/// 玩家 怪物这样的角色
/// </summary>
public abstract class FSMController<T> : MonoBehaviour
{
    //当前状态
    //作为类型名引用静态方法
    public T CurrentState;

    //当前的状态对象
    protected StateBase<T> CurrStateObj;


    //存放全部状态对象 -对象池 T表示类型 可能是PlayerState 可能是MonsterState
    //private List<StateBase<T>> stateList = new List<StateBase<T>>(); 

    private Dictionary<T, StateBase<T>> stateDic = new Dictionary<T, StateBase<T>>();

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="newState">新的状态</param>
    /// <param name="reCurrState">是否需要刷新状态</param>
    public void ChangeState<K>(T newState, bool reCurrState = false) where K : StateBase<T>, new()
    {
        //如果新状态和当前状态一致 同时并不需要刷新状态
        if (newState.Equals(CurrentState) && !reCurrState) return;

        //如果当前状态存在，应该执行其的退出
        if (CurrStateObj != null) CurrStateObj.OnExit();

        //基于新状态的枚举 获得一个新的状态对象
        CurrStateObj = GetStateObj<K>(newState);
        CurrStateObj.OnEnter();//初始化
        Debug.Log("当前的状态是" + newState.ToString());

    }

    /// <summary>
    /// 获取状态对象
    /// 你给我一个枚举，我返回一个和这个枚举同名的类型的对象
    /// 保证不会返回null
    /// where 表示约束泛型
    /// </summary>
    /// <returns></returns>
    private StateBase<T> GetStateObj<K>(T stateType) where K:StateBase<T>,new()
    {
        if(stateDic.ContainsKey(stateType)) return stateDic[stateType];

        //到这里 说明库里没有
        //实例化一个并且返回
        //StateBase<T> state = Activator.CreateInstance(Type.GetType(stateType.ToString())) as StateBase;//这里使用反射 根据类型动态创建对象

        StateBase<T> state = new K();
        state.Init(this,stateType);//初始化

        stateDic.Add(stateType, state);
        Debug.Log("加入了" + state.ToString() + "状态");
        return state;
    }

    protected virtual void Update()
    {
        if (CurrStateObj != null) CurrStateObj.OnUpdate();
        //Debug.Log("更新");
    }
}
