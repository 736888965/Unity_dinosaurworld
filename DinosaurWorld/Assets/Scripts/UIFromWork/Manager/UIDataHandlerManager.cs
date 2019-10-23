using System;
using System.Collections.Generic;

public abstract class UIDataHandlerManager : IUIDataHandlerManager
{
    protected Dictionary<string, IDataHande> handlerDic;

    public UIDataHandlerManager()
    {
        handlerDic = new Dictionary<string, IDataHande>();
        RegisterHandler();
    }
    protected abstract void RegisterHandler();

    public void RemoveHandler(string handlerName)
    {
        handlerDic.Remove(handlerName);
    }
    public IDataHande GetHandler(string handlerName)
    {
        if (handlerDic.ContainsKey(handlerName))
            return handlerDic[handlerName];
        else
            throw new Exception("this proxy is not registered");
    }
}

