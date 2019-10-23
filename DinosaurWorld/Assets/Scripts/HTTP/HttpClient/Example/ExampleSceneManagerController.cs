using System.IO;
using CI.HttpClient;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExampleSceneManagerController : MonoBehaviour
{
    public Text LeftText;
    public Text RightText;
    public Slider ProgressSlider;

    public void Upload()
    {
        HttpClient client = new HttpClient();

        byte[] buffer = new byte[1000000];
        new System.Random().NextBytes(buffer);

        ByteArrayContent content = new ByteArrayContent(buffer, "application/bytes");

        ProgressSlider.value = 0;

        client.Post(new System.Uri("http://httpbin.org/post"), content, HttpCompletionOption.AllResponseContent, (r) =>
        {           
        }, (u) =>
        {
            LeftText.text = "Upload: " +  u.PercentageComplete.ToString() + "%";
            ProgressSlider.value = u.PercentageComplete;
        });
    }

    public void Download()
    {
        HttpClient client = new HttpClient();

        ProgressSlider.value = 100;

        client.Get(new System.Uri("http://httpbin.org/bytes/1000000"), HttpCompletionOption.StreamResponseContent, (r) =>
        {
            RightText.text = "Download: " + r.PercentageComplete.ToString() + "%";
            ProgressSlider.value = 100 - r.PercentageComplete;
        });
    }

    public void UploadDownload()
    {
        HttpClient client = new HttpClient();

        byte[] buffer = new byte[1000000];
        new System.Random().NextBytes(buffer);

        ByteArrayContent content = new ByteArrayContent(buffer, "application/bytes");

        ProgressSlider.value = 0;

        client.Post(new System.Uri("http://httpbin.org/post"), content, HttpCompletionOption.StreamResponseContent, (r) =>
        {
            RightText.text = "Download: " + r.PercentageComplete.ToString() + "%";
            ProgressSlider.value = 100 - r.PercentageComplete;
        }, (u) =>
        {
            LeftText.text = "Upload: " + u.PercentageComplete.ToString() + "%";
            ProgressSlider.value = u.PercentageComplete;
        });
    }

    public void Delete()
    {
        HttpClient client = new HttpClient();
        client.Delete(new System.Uri("http://httpbin.org/delete"), HttpCompletionOption.AllResponseContent, (r) =>
        {
#pragma warning disable 0219
            string responseData = r.ReadAsString();
#pragma warning restore 0219
        });
    }

    public void Get()
    {
        HttpClient client = new HttpClient();
        client.Get(new System.Uri("http://httpbin.org/get"), HttpCompletionOption.AllResponseContent, (r) =>
        {
#pragma warning disable 0219
            byte[] responseData = r.ReadAsByteArray();
#pragma warning restore 0219
        });
    }

    public void Patch()
    {
        HttpClient client = new HttpClient();

        StringContent content = new StringContent("Hello World");

        client.Patch(new System.Uri("http://httpbin.org/patch"), content, HttpCompletionOption.AllResponseContent, (r) =>
        {
#pragma warning disable 0219
            Stream responseData = r.ReadAsStream();
#pragma warning restore 0219
        });
    }

    public void Post()
    {
        HttpClient client = new HttpClient();

       // StringContent content = new StringContent("Hello World");

        StringContent content = new StringContent("@@@@@@", System.Text.Encoding.UTF8, "application/json");
        client.Post(new System.Uri("http://httpbin.org/post"), content, HttpCompletionOption.AllResponseContent, (r) =>
        {
#pragma warning disable 0219
            string responseData = r.ReadAsString();
            Debug.LogError(responseData);
#pragma warning restore 0219
        });
    }

    public void Put()
    {
        HttpClient client = new HttpClient();

        StringContent content = new StringContent("Hello World");

        client.Put(new System.Uri("http://httpbin.org/put"), content, HttpCompletionOption.AllResponseContent, (r) =>
        {
#pragma warning disable 0219
            string responseData = r.ReadAsString();
#pragma warning restore 0219
        });
    }

    public void PostTest()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("userName", "18810085117");
        dic.Add("passWord", "1245676");
        dic.Add("equipment", "huawei");
        dic.Add("timeStamp", 1234567);

        HttpClient client = new HttpClient();
        string json = MiniJSON.Json.Serialize(dic);
        StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "mtext/plain");
        client.Post(new System.Uri(WebRequestAPI.Login), content, HttpCompletionOption.StreamResponseContent, (r) =>
        {
#pragma warning disable 0219
            string responseData = r.ReadAsString();
            Debug.Log(r.IsSuccessStatusCode);
            Debug.LogError(responseData);
            Debug.LogError(r.OriginalRequest.Address);
            Dictionary<string, object> list = MiniJSON.Json.Deserialize(responseData) as Dictionary<string, object>;
            foreach (var item in list.Keys)
            {
                Debug.LogError(item);
                Debug.LogError(list[item]);
            }
#pragma warning restore 0219
        });

    }

}