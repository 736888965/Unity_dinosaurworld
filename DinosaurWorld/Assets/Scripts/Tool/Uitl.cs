using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Uitl
{

    public static long GetTimestamp()
    {
        DateTime dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        // Debug.Log(dt1970.Ticks);
        return (DateTime.UtcNow.Ticks - dt1970.Ticks) / 10000 / 1000;
    }
    const string datapath = "data.md";

    public static void AddData(string data)
    {
        string pathDir = Application.persistentDataPath + "/Base/";
        string path = pathDir + datapath;
        Debug.LogError(path);
        if (!Directory.Exists(pathDir))
        {
            Directory.CreateDirectory(pathDir);
        }
        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.Write(data + "\n");
            sw.Flush();
            sw.Close();
        }
        if (File.Exists(path))
        {
            Debug.LogError(data);
        }
    }

    public static void DeleteData(string data)
    {
        Debug.LogError(data);
        string pathDir = Application.persistentDataPath + "/Base/";
        string path = pathDir + datapath;
        if (File.Exists(path))
        {
            string[] strs = File.ReadAllLines(path);
            List<string> list = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (string.IsNullOrEmpty(strs[i]))
                    continue;
                // string[] times = strs[i].Split('|');
                if (strs[i].StartsWith(data))
                {
                    Debug.Log("DELETE:" + strs[i]);
                    continue;
                }
                list.Add(strs[i]);
            }
            File.WriteAllLines(path, list.ToArray());
            if (list.Count > 0)
                AgainSend(list[0]);
        }
    }

    public static void AgainSend(string data)
    {
        string[] send = data.Split('|');
        if (send.Length > 2)
        {
            string url = string.Empty;
            switch (send[1])
            {
                case WebType.fabulous:
                    if (send.Length == 4)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add(WebParameter.userId, int.Parse(send[2]));
                        dic.Add(WebParameter.dID, send[3]);
                        dic.Add(WebParameter.timeStamp, long.Parse(send[0]));
                        WebCallBack call = new WebCallBack(long.Parse(send[0]), null);
                        HttpRequestBase.Instance.Post(WebRequestAPI.Fabulous, dic, call);
                    }
                    break;
                case WebType.unFabulous:
                    if (send.Length == 5)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add(WebParameter.userId, int.Parse(send[2]));
                        dic.Add(WebParameter.dID, send[3]);
                        dic.Add(WebParameter.fabulousId, send[4]);
                        dic.Add(WebParameter.timeStamp, long.Parse(send[0]));
                        WebCallBack call = new WebCallBack(long.Parse(send[0]), null);
                        HttpRequestBase.Instance.Post(WebRequestAPI.unFabulous, dic, call);
                    }
                    break;
                case WebType.scanning:
                    Debug.Log("scanning : " + data);
                    if (send.Length == 4)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add(WebParameter.userId, int.Parse(send[2]));
                        dic.Add(WebParameter.dID, send[3]);
                        dic.Add(WebParameter.timeStamp, long.Parse(send[0]));
                        WebCallBack call = new WebCallBack(long.Parse(send[0]), null);
                        HttpRequestBase.Instance.Post(WebRequestAPI.Scanning, dic, call);
                    }
                    break;
                case WebType.scanningOut:
                    Debug.Log("scanningOut : " + data);
                    if (send.Length == 5)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add(WebParameter.userId, int.Parse(send[2]));
                        dic.Add(WebParameter.dID, send[3]);
                        dic.Add(WebParameter.scanningId, int.Parse(send[4]));
                        dic.Add(WebParameter.timeStamp, long.Parse(send[0]));
                        WebCallBack call = new WebCallBack(long.Parse(send[0]), null);
                        HttpRequestBase.Instance.Post(WebRequestAPI.Scanning, dic, call);
                    }
                    break;
                default:
                    Debug.Log("url time  write is error;");
                    DeleteData(send[0]);
                    break;
            }
        }
    }

}
