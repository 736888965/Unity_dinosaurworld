using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public interface IUIManager
{
    /// <summary>
    /// add UI init delegate
    /// </summary>
    /// <param name="action"></param>
    void AddUIInitListener(Func<string, Object, bool> action);

    /// <summary>
    /// add UI show or hide delegate
    /// </summary>
    /// <param name="action"></param>
    void AddUIActiveListener(Func<string, bool, bool> action);

    /// <summary>
    /// 根据UI的id显示UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    void ShowUI<T>(T id);

    /// <summary>
    /// 返回上一见面
    /// </summary>
    void Back();
}

