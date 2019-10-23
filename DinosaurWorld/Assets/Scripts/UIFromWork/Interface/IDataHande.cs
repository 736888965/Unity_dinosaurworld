using System;
using System.Collections.Generic;

public interface  IDataHande
{
    /// <summary>
    /// 更新UI显示
    /// </summary>
    Action UpdateShow { get; set; }

    /// <summary>
    /// 获取处理器的名称
    /// </summary>
    /// <returns></returns>
    string GetName();

    /// <summary>
    ///  初始化数据对象
    /// </summary>
    void InitData();
    /// <summary>
    /// 获取数据对象
    /// </summary>
    /// <returns></returns>
    IData GetData();

    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="newData"></param>
    void UpdataData(IData newData);
}

