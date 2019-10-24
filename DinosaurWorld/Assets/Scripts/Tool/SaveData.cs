using System;
using System.Collections.Generic;
using UnityEngine;
public class SaveData
{
    /// <summary>
    /// 账号
    /// </summary>
    private const string Account = "DinosaurWorld_Account";

    private const string PassWorld = "DinosaurWorld_PassWorld";

    /// <summary>
    /// 本地保存点赞信息
    /// </summary>
    private const string DinosaurWorld_State = "DinosaurWorld_State";


    public static string GetAccount()
    {
        if(PlayerPrefs.HasKey(Account))
        {
            return PlayerPrefs.GetString(Account);
        }
        return null;
    }

    public static void SaveAccount(string account)
    {
        PlayerPrefs.SetString(Account, account);
    }
    public static string GetPass()
    {
        if (PlayerPrefs.HasKey(PassWorld))
        {
            return PlayerPrefs.GetString(PassWorld);
        }
        return null;
    }

    public static void SavePass(string pass)
    {
        PlayerPrefs.SetString(PassWorld, pass);
    }

    public static bool  IsLogin()
    {
        return PlayerPrefs.HasKey(Account) && PlayerPrefs.HasKey(PassWorld);
    }

    /// <summary>
    /// 设置保存点赞的名称
    /// </summary>
    /// <param name="name"></param>
    /// <param name="state"></param>
    public static void SetState(string name, bool state)
    {
        string temp = string.Empty;
        if (PlayerPrefs.HasKey(DinosaurWorld_State))
        {
            temp = PlayerPrefs.GetString(DinosaurWorld_State);
        }
        if(state)
        {
            if (temp.Contains(name + ";"))
                return;
            else
                temp += name + ";";
        }
        else
            temp = temp.Replace(name + ";", "");
        PlayerPrefs.SetString(DinosaurWorld_State, temp);
    }

    /// <summary>
    /// 获取所有点赞保存的名称
    /// </summary>
    /// <returns></returns>
    public static List<string> GetState()
    {
        string temp = string.Empty;
        List<string> list = new List<string>();
        if (PlayerPrefs.HasKey(DinosaurWorld_State))
        {
            temp = PlayerPrefs.GetString(DinosaurWorld_State);
            string[] states = temp.Split(';');
            for (int i = 0; i < states.Length; i++)
            {
                Debug.Log( states[i]);
                if (!list.Contains(states[i])&&!string.IsNullOrEmpty(states[i]))
                    list.Add(states[i]);
            }
        }
        return list;
    }

    /// <summary>
    /// 获取单个恐龙是否点赞
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool GetSingleState(string name)
    {
        string temp = string.Empty;
        if (PlayerPrefs.HasKey(DinosaurWorld_State))
        {
            temp = PlayerPrefs.GetString(DinosaurWorld_State);
        }
        return temp.Contains(name + ";");
    }

}

