using UnityEngine;
using System.Collections;
using Vuforia;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// 点击屏幕，进行自动对焦
/// </summary>
public class AutoCamera : MonoBehaviour
{
    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    


    private void OnVuforiaStarted()
    {
        Vuforia.CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);//全自动
    }
    /// <summary>
    /// 暂停程序的时候，再次激活
    /// </summary>
    /// <param name="obj"></param>
    private void OnPaused(bool obj)
    {
        Vuforia.CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);//全自动

    }



    // Update is called once per frame
    /// <summary>
    /// 可以在updat里写一个点击屏幕，就运行下自动对焦
    /// </summary>
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                Debug.Log("click object name is ---->" + Input.mousePosition);
                return;
            }

        }
        if (Input.touchCount > 0)
        {
            Vuforia.CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);//目标自动对焦
        }
    }
}
