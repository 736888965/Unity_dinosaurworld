using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
public interface IUIEffectManager
{
    /// <summary>
    /// ui init event
    /// </summary>
    /// <param name="uiId"></param>
    /// <param name="uiBase"></param>
    /// <returns></returns>
    bool InitFun(string uiId, Object uiBase);
    /// <summary>
    /// trigger ui show or hide event
    /// </summary>
    /// <param name="uiId"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    bool ActiveFun(string uiId, bool isActive);
}

