using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{

    AsyncOperation asyn;
    //进度条的数值
    private float progressValue;
    //进度条 加载中.....
    public Slider slider;
    //显示进度的文本
    public Text progress;
    private void Start()
    {
        HttpRequestBase.Instance.Init();
        ResoureManager.Instance.Load();
        ResoureManager.Instance.Login();
        //SceneManager.LoadScene("AR");
        Screen.sleepTimeout = 0;
       StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        asyn = SceneManager.LoadSceneAsync("AR");
        asyn.allowSceneActivation = false;
        yield return asyn;
    }
    private void Update()
    {
        if (asyn != null)
        {
            Debug.Log(asyn.progress);
            if (asyn.progress >= 0.9f)
                asyn.allowSceneActivation = true;
            if (asyn.progress < 0.9f)
                progressValue = asyn.progress;
            else
                progressValue = 1.0f;
            slider.value = progressValue;
            progress.text = "加载中..... " + (int)(slider.value * 100) + " %";
        }
    }
}
