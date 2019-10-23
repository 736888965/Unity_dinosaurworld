using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : SingletonMono<AudioSourceManager>
{


    List<AudioSource> mAudioSourceList = new List<AudioSource>();



    public void Close()
    {
        for (int i = 0; i < mAudioSourceList.Count; i++)
        {
            mAudioSourceList[i].Stop();
            Debug.Log("关闭" + i);
        }
    }
    public void CloseModelMessage()
    {
        for (int i = 0; i < mAudioSourceList.Count; i++)
        {
            if (int.Parse(mAudioSourceList[i].clip.name) > 100)
                mAudioSourceList[i].Stop();
        }
    }

    public void PauseModelMessage()
    {
        for (int i = 0; i < mAudioSourceList.Count; i++)
        {
            if (int.Parse(mAudioSourceList[i].clip.name) > 100)
                mAudioSourceList[i].Pause();
        }
    }
    public void PlayModelMessage()
    {
        for (int i = 0; i < mAudioSourceList.Count; i++)
        {
            if (int.Parse(mAudioSourceList[i].clip.name) > 100)
                mAudioSourceList[i].Play();
        }
    }

    public void SetModelAnimation(string path)
    {
        AudioClip clip = TransfromHelp.ResourceMusic(path);
        AudioSource audio = GetAudioSource();
        audio.clip = clip;
    }

    public void OpenModelAnimation(string path)
    {
        AudioClip clip = TransfromHelp.ResourceMusic(path);
        AudioSource audio = GetAudioSource();
        audio.clip = clip;
        audio.Play();
    }


    private AudioSource GetAudioSource()
    {
        AudioSource res = null;
        for (int i = 0; i < mAudioSourceList.Count; i++)
        {
            if (!mAudioSourceList[i].isPlaying)
            {
                res = mAudioSourceList[i];
                break;
            }
        }
        if (res == null)
        {
            res = gameObject.AddComponent<AudioSource>();
            mAudioSourceList.Add(res);
        }
        return res;
    }
}
