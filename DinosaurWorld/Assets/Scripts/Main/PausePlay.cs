using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePlay : MonoBehaviour {

    public GameObject play;
    public GameObject pause;

    private void OnEnable()
    {
        Init(true);
    }
    private void Init(bool isactive)
    {
        play.SetActive(isactive);
        pause.SetActive(!isactive);
    }

    public void Play( )
    {
        Init(true);
        AudioSourceManager.Instance.PlayModelMessage();
    }

    public void Stop()
    {
        Init(false);
        AudioSourceManager.Instance.PauseModelMessage();
    }
}