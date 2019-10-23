using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioSource message;
    public AudioSource model;


    public void SetModel(string str)
    {
        AudioClip clip = TransfromHelp.ResourceMusic(str);
        model.clip = clip;
        model.Play();
    }

    public void SetMessage(string str)
    {
        AudioClip clip = TransfromHelp.ResourceMusic(str);
        message.clip = clip;
      
    }

    public void Close()
    {
        model.Stop();
        message.Stop();
    }

    public void SetMessagePaure()
    {
        message.Pause();
    }

    public void SetMessagePlay()
    {
        message.Play();
    }
    public void CloseMessage()
    {
        message.Stop();
    }
}
