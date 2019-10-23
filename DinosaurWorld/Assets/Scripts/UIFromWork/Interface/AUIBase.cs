using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public abstract class AUIBase:MonoBehaviour
{
    public string ID { get; set; }



    public UIStateEnum UIState
    {
        get { return GetUIState(); }
        set { SetUIState(value); }
    }

    /// <summary>
    /// 初始化函数监听
    /// </summary>
    /// <param name="active"></param>
    public abstract void AddInitListener(Func<Object, bool> action);

    /// <summary>
    /// 添加对象显示或隐藏状态的监听
    /// </summary>
    /// <param name="action"></param>
    public abstract void AddInitListener(Func<bool, bool> action);

    /// <summary>
    /// 获取UI的层级
    /// </summary>
    /// <returns></returns>
    public abstract UILayer Getlayer();

    /// <summary>
    /// 获取数据管理对象
    /// </summary>
    /// <returns></returns>
    protected abstract IUIDataHandlerManager GetDataHandlerManager();

    /// <summary>
    /// 设置UI show
    /// </summary>
    /// <param name="isShow"></param>
    protected abstract void SetActive(bool isShow);

    /// <summary>
    /// 设置UI show -> hide
    /// </summary>
    /// <param name="state"></param>
    protected abstract void SetUIState(UIStateEnum state);
    /// <summary>
    /// 设置UI的层级
    /// </summary>
    /// <returns></returns>
    protected abstract UIStateEnum GetUIState();

    /// <summary>
    /// UI状态改变处理事件
    /// </summary>
    /// <param name="currentState"></param>
    /// <param name="targetState"></param>
    protected abstract void HandlerState(UIStateEnum currentState, UIStateEnum targetState);
}