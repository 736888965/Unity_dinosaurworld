using CI.HttpClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WebCallBack
{
    public long tag;
    public Action<string> callBack;

    public WebCallBack(long tag, Action<string> callBack)
    {
        this.tag = tag;
        this.callBack = callBack;
    }
}



public class HttpRequestBase : SingletonMono<HttpRequestBase>
{

    public static string httpHost = "http://dinosaurar.ch-view.com/";

    HttpClient client;

    private Dictionary<string, List<WebCallBack>> web_CallBack;



    public void Init()
    {
        client = new HttpClient();
        web_CallBack = new Dictionary<string, List<WebCallBack>>();
    }


    public void AddCallBack(string url, WebCallBack callback)
    {

        Debug.LogError(url + callback==null);
        if (web_CallBack.ContainsKey(url))
        {
            List<WebCallBack> list = web_CallBack[url];
            bool isHave = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].tag == callback.tag)
                {
                    isHave = true;
                    break;
                }
            }
            if (!isHave)
                web_CallBack[url].Add(callback);
        }
        else
        {
            web_CallBack[url] = new List<WebCallBack>();
            web_CallBack[url].Add(callback);
        }
    }

    private void DeleteCallBack(string url, long tag)
    {
        Debug.LogError(tag);
        if (web_CallBack.ContainsKey(url))
        {
            List<WebCallBack> list = web_CallBack[url];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].tag == tag)
                {
                    web_CallBack[url].Remove(list[i]);
                    break;
                }
            }
            if (web_CallBack[url].Count <= 0)
                web_CallBack.Remove(url);
        }
    }


    private void TriggerCallBack(string url, long tag, string json)
    {
        if (web_CallBack.ContainsKey(url))
        {
            List<WebCallBack> list = web_CallBack[url];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].tag == tag)
                {
                    if (list[i].callBack!= null)
                        list[i].callBack(json);
                    web_CallBack[url].Remove(list[i]);
                    Uitl.DeleteData(tag.ToString());
                    break;
                }
            }
            if (web_CallBack[url].Count <= 0)
                web_CallBack.Remove(url);
        }
    }

    string code = "code";
    string tag = "tag";
    string data = "data";
    string equipment = "equipment";
    string equipmentId = "equipmentId";
    string timeStamp = "timeStamp";
    public void Post(string url, IDictionary<string, object> message, WebCallBack callback=null)
    {
        if (callback != null)
            AddCallBack(url, callback);
        Debug.LogError(SystemInfo.deviceUniqueIdentifier);
        message.Add(equipment, SystemInfo.deviceName);
        message.Add(equipmentId, SystemInfo.deviceUniqueIdentifier);
        string json = MiniJSON.Json.Serialize(message);
        StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        client.Post(new Uri(httpHost + url), content, HttpCompletionOption.AllResponseContent, (r) =>
             {
                 bool state = r.IsSuccessStatusCode;
                 if (state) // success
                 {
                     string address = r.OriginalRequest.Address.ToString().Replace(httpHost, "");
                     string responseData = r.ReadAsString();
                     Debug.LogError(responseData);
                     if (!string.IsNullOrEmpty(responseData))
                     {
                         Dictionary<string, object> result = MiniJSON.Json.Deserialize(responseData) as Dictionary<string, object>;
                         if (result.ContainsKey(code))
                         {
                             if (string.Equals(result[code], "1"))
                                 return;
                             if (result.ContainsKey(data))
                             {
                                 string str = MiniJSON.Json.Serialize(result[data]);
                                 if (!string.IsNullOrEmpty(str))
                                 {
                                     Dictionary<string, object> datajson = MiniJSON.Json.Deserialize(str) as Dictionary<string, object>;
                                     if (datajson != null && datajson.ContainsKey(timeStamp))
                                     {
                                         long value = (long)datajson[timeStamp];
                                         int codeRes = 0;
                                         if (result.ContainsKey(code))
                                         {
                                             if (int.TryParse(MiniJSON.Json.Serialize(result[code]), out codeRes))
                                             {
                                                 if (codeRes != 1)//fail
                                                     DeleteCallBack(address, value);
                                                 else
                                                     TriggerCallBack(address, value, str);
                                             }
                                         }
                                     }
                                 }
                             }
                         }
                     }
                 }
                 else
                 {
                     string responseData = r.ReadAsString();
                     Debug.LogError("fail ： " + responseData);
                 }
             });
    }


}
