using System;
using System.Collections.Generic;

public interface IUILayerManager
{
    /// <summary>
    /// 初始化此层级管理器
    /// </summary>
    void Init();

    /// <summary>
    /// 设置UI到其对应的层级父物体下
    /// </summary>
    /// <param name="ui"></param>
    void SetUILayer(AUIBase ui);
}

