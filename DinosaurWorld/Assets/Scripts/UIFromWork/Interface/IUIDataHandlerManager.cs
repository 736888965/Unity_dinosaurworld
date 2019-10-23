using System;
using System.Collections.Generic;

/// <summary>
/// 数据管理对象
/// </summary>
public interface IUIDataHandlerManager
{
    /// <summary>
    /// 根据名称移除数据处理器
    /// </summary>
    /// <param name="handlerName"></param>
    void RemoveHandler(string handlerName);

    /// <summary>
    /// 获取数据管理对象
    /// </summary>
    /// <param name="handleName"></param>
    /// <returns></returns>
    IDataHande GetHandler(string handleName);
}

