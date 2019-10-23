using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoureManager : SingletonMono<ResoureManager>
{
    /// <summary>
    ///  iD   info
    /// </summary>
    public Dictionary<int, InfoCode> m_InfoList = new Dictionary<int, InfoCode>();

    /// <summary>
    ///  name  ID
    /// </summary>
    public Dictionary<string, int> InfoIdList = new Dictionary<string, int>();
    public Dictionary<string, IdName> InfoIdNameList = new Dictionary<string, IdName>();

    public void Load()
    {
        //  StartCoroutine(LoadWWW());
        LoadConfig();
    }

    IEnumerator LoadWWW(Action action = null)
    {

        WWW www = new WWW("file://" + Application.streamingAssetsPath + Gloab.InfoPath);
        yield return www;

        //Debug.Log(www.text);
        InfoList list = JsonUtility.FromJson<InfoList>(www.text);
        int id;
        foreach (InfoCode item in list.infoList)
        {
            id = item.id;
            if (!m_InfoList.ContainsKey(id))
                m_InfoList.Add(id, item);
        }
        if (action != null)
            action();
    }
    void LoadConfig()
    {
        TextAsset asset = Resources.Load("Configs/Info") as TextAsset;
        //Debug.Log(asset);
        InfoList list = JsonUtility.FromJson<InfoList>(asset.text);
        int id;
        foreach (InfoCode item in list.infoList)
        {
            //Debug.LogError(item.info);
            //Debug.LogError(item.id);
            id = item.id;
            if (!m_InfoList.ContainsKey(id))
                m_InfoList.Add(id, item);
        }


        asset = Resources.Load("Configs/IDInfo") as TextAsset;
        //Debug.Log(asset);
        IdNameList idName = JsonUtility.FromJson<IdNameList>(asset.text);
        string name = null;
        foreach (IdName item in idName.infoList)
        {
            //Debug.LogError(item.name);
            //Debug.LogError(item.id);
            name = item.name;
            if (!InfoIdList.ContainsKey(name))
                InfoIdList.Add(name, item.id);
            if (!InfoIdNameList.ContainsKey(name))
                InfoIdNameList.Add(name, item);

        }
    }

    #region 点赞

    public List<string> list = new List<string>();
    public void Add(string name)
    {
        if (!list.Contains(name))
            list.Add(name);
        SaveData.SetState(name, true);
        int modelId = 0;
        if (InfoIdList.TryGetValue(name, out modelId))
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(WebParameter.userId, Gloab.userID);
            dic.Add(WebParameter.dID, modelId);
            long time = Uitl.GetTimestamp();
            dic.Add(WebParameter.timeStamp, time);
            string data = time + "|" + WebType.fabulous + "|" + Gloab.userID + "|" + modelId;
            Uitl.AddData(data);
            WebCallBack call = new WebCallBack(time, Fabulous);
            //HttpRequestBase.Instance.AddCallBack(WebRequestAPI.Fabulous, call);
            HttpRequestBase.Instance.Post(WebRequestAPI.Fabulous, dic, call);
        }
    }

    private void Fabulous(string obj)
    {
        Debug.Log("点赞");
        Dictionary<string, object> dic = MiniJSON.Json.Deserialize(obj) as Dictionary<string, object>;
        string sacnnin = string.Empty;
        if (dic.ContainsKey(WebParameter.fabulousId))
        {
            Debug.Log("fabulousId :" + dic[WebParameter.fabulousId]);
            int id = 0;
            if (int.TryParse(dic[WebParameter.fabulousId].ToString(), out id))
            {
                Debug.Log("fabulousId :" + id);
                Gloab.fabulousId = id;
            }
        }
    }

    public void Delete(string name)
    {
        if (list.Contains(name))
            list.Remove(name);
        SaveData.SetState(name, false);
        int modelId = 0;
        if (InfoIdList.TryGetValue(name, out modelId))
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(WebParameter.userId, Gloab.userID);
            dic.Add(WebParameter.dID, modelId);
            dic.Add(WebParameter.fabulousId, Gloab.fabulousId);
            long time = Uitl.GetTimestamp();
            dic.Add(WebParameter.timeStamp, time);
            string data = time + "|" + WebType.unFabulous + "|" + Gloab.userID + "|" + modelId + "|" + Gloab.fabulousId;
            Uitl.AddData(data);
            WebCallBack call = new WebCallBack(time, null);
            HttpRequestBase.Instance.Post(WebRequestAPI.Fabulous, dic, call);
        }
    }



    #endregion




    #region 识别
    public void ScannModel(string name)
    {
        int modelId = 0;
        if (InfoIdList.TryGetValue(name, out modelId))
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(WebParameter.userId, Gloab.userID);
            dic.Add(WebParameter.dID, modelId);
            long time = Uitl.GetTimestamp() + 1;
            dic.Add(WebParameter.timeStamp, time);
            string data = time + "|" + WebType.scanning + "|" + Gloab.userID + "|" + modelId;
            Uitl.AddData(data);
            WebCallBack call = new WebCallBack(time, SanningID);
            HttpRequestBase.Instance.Post(WebRequestAPI.Scanning, dic, call);
        }
    }
    public void ScannOutModel(string name)
    {
        int modelId = 0;
        if (InfoIdList.TryGetValue(name, out modelId))
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(WebParameter.userId, Gloab.userID);
            dic.Add(WebParameter.dID, modelId);
            dic.Add(WebParameter.scanningId, Gloab.scanning);
            long time = Uitl.GetTimestamp();
            dic.Add(WebParameter.timeStamp, time);
            string data = time + "|" + WebType.scanningOut + "|" + Gloab.userID + "|" + modelId + "|" + Gloab.scanning;
            Uitl.AddData(data);
            WebCallBack call = new WebCallBack(time, SanningID);
            HttpRequestBase.Instance.Post(WebRequestAPI.ScanningOut, dic, call);
        }
    }

    private void SanningID(string obj)
    {
        //Debug.LogError("扫描");
        Dictionary<string, object> dic = MiniJSON.Json.Deserialize(obj) as Dictionary<string, object>;
        string sacnnin = string.Empty;
        if (dic.ContainsKey(WebParameter.scanningId))
        {
            Debug.Log("sacnnin :" + dic[WebParameter.scanningId]);
            int id = 0;
            if (int.TryParse(dic[WebParameter.scanningId].ToString(), out id))
            {
                Debug.Log("scanningID :" + id);
                Gloab.scanning = id;
            }
        }
    }


    #endregion
    public string GetName(string name)
    {
        if (InfoIdList.ContainsKey(name))
        {
            int id = InfoIdList[name];
            if (m_InfoList.ContainsKey(id))
                return m_InfoList[id].name;
        }
        return null;
    }

    private string user;
    private string pass;

    public void Send(string userName, string passWorld)
    {

        user = userName;
        pass = passWorld;
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add(WebParameter.userName, userName);
        dic.Add(WebParameter.passWorld, passWorld);
        long time = Uitl.GetTimestamp();
        dic.Add(WebParameter.timeStamp, time);
        WebCallBack call = new WebCallBack(time, SendCallBack);
        HttpRequestBase.Instance.Post(WebRequestAPI.Register, dic, call);
    }

    private void SendCallBack(string obj)
    {
        Dictionary<string, object> dic = MiniJSON.Json.Deserialize(obj) as Dictionary<string, object>;
       
        if (dic.ContainsKey(WebParameter.userId))
        {
            Debug.Log("register :" + dic[WebParameter.userId]);
            int id = 0;
            if (int.TryParse(dic[WebParameter.userId].ToString(), out id))
            {
                Debug.Log("register :" + id);
                Gloab.userID = id;
                SaveData.SaveAccount(user);
                SaveData.SavePass(pass);
            }
        }

        //if (dic.ContainsKey(WebParameter.loginLogId))
        //{
        //    Debug.Log("loginLogId :" + dic[WebParameter.loginLogId]);
        //    int id = 0;
        //    if (int.TryParse(dic[WebParameter.loginLogId].ToString(), out id))
        //    {
        //        Debug.Log("loginLogId :" + id);
        //        Gloab.loginLoad = id;
        //        SaveData.SavePass(pass);
        //    }
        //}

        Login();
    }


    public void Login()
    {
        //Debug.LogError(SaveData.GetAccount() + "|||||||||||||" + SaveData.GetPass());
        if (SaveData.IsLogin())
        {
            //Debug.LogError(SaveData.GetAccount() + "|||||||||||||" + SaveData.GetPass());
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(WebParameter.userName, SaveData.GetAccount());
            dic.Add(WebParameter.passWorld, SaveData.GetPass());
            long time = Uitl.GetTimestamp();
            dic.Add(WebParameter.timeStamp, time);
            WebCallBack call = new WebCallBack(time, Logining);
            HttpRequestBase.Instance.Post(WebRequestAPI.Login, dic, call);
        }
    }
    private void Logining(string obj)
    {
        Dictionary<string, object> dic = MiniJSON.Json.Deserialize(obj) as Dictionary<string, object>;

        if (dic.ContainsKey(WebParameter.userId))
        {
            Debug.Log("register :" + dic[WebParameter.loginLogId]);
            int id = 0;
            if (int.TryParse(dic[WebParameter.loginLogId].ToString(), out id))
            {
                Debug.Log("register :" + id);
                Gloab.loginLogId = id;
            }
        }

    }


    public void LoginQut()
    {
        if(Gloab.loginLogId!=0)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(WebParameter.loginLogId, Gloab.loginLogId);
            HttpRequestBase.Instance.Post(WebRequestAPI.LoginOut, dic);
        }
    }

    public void OnApplicationQuit()
    {
        LoginQut();
    }
}
