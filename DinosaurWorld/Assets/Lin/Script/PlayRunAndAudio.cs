using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayRunAndAudio : MonoBehaviour
{

    // Use this for initialization
    public DOTweenPath path;
    public AudioSource audioPlay;
    void Awake()
    {
        path = transform.GetComponent<DOTweenPath>();
        if (path!=null)
        {
            print(path.transform.name);

        }
        
       // audioPlay = transform.GetComponent<AudioSource>();

    }
    private void OnEnable()
    {
        Initialize();
    }

    //初始化
    public void Initialize()
    {

        DOTweenPathInitialize();
    }



    /// <summary>
    /// 重置路径动画
    /// </summary>
    public void DOTweenPathInitialize()
    {
        if(path!=null)
        {
            path.DORestart();
            path.DOPause();
        }
       
    }

    /// <summary>
    /// 播放doTween路径动画
    /// </summary>
    public void Run()
    {
        if (path != null)
        {
            print("播放路径动画");
            path.DOPlay();
        }
        
    }
    /// 暂停doTween路径动画
    public void StopRun()
    {
        if(path!=null)
        {
            print("停止路径动画");
            path.DOPause();
        }
    }

    public void Play_1()
    {
        if (audioPlay.clip != null)
        {
            audioPlay.Play();
            print("播放声音");
        }
        else
        {
            print("没有声音");
        }

    }
}
