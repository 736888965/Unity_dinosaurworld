using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUIPathManager
{
    protected static Dictionary<string, string> UIPathDic = new Dictionary<string, string>();

    protected abstract void InitPathDic();

    public AUIPathManager()
    {

        InitPathDic();
    }

    public static string GetPath(string id)
    {
        if (UIPathDic.ContainsKey(id))
            return UIPathDic[id];
        else
        {
            Debug.LogError("未在UIPathManager初始化该UI");
            return null;
        }
    }
}

