using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{

    public InputField input_iphone;

    public InputField input_pass;

    public Button but_send;

    public Button but_term;

    public RectTransform messageTF;
    public Button tfbut;
    public float timeMove = 0.2f;
    public RectTransform logingPanel;
    public Button close;
    private void Start()
    {
        but_send.onClick.AddListener(Send);
        but_term.onClick.AddListener(ShowTerm);
        //message.gameObject.SetActive(false);
        tfbut.onClick.AddListener(CloseMessage);
        close.onClick.AddListener(Close);
       /* Debug.LogError(SystemInfo.deviceType.ToString() + "$$$$$$$$$$$$$$" + SystemInfo.deviceName);


        

        Debug.LogError("设备模型"+ SystemInfo.deviceModel);

        //设备的名称

        Debug.LogError("设备名称"+ SystemInfo.deviceName);

        //设备的类型

        Debug.LogError("设备类型（PC电脑，掌上型）" + SystemInfo.deviceType.ToString());

        //系统内存大小

        Debug.LogError("系统内存大小MB" + SystemInfo.systemMemorySize.ToString());

        //操作系统

        Debug.LogError("操作系统" + SystemInfo.operatingSystem);

        //设备的唯一标识符

        Debug.LogError("设备唯一标识符" + SystemInfo.deviceUniqueIdentifier);

        //显卡设备标识ID

        Debug.LogError("显卡ID" + SystemInfo.graphicsDeviceID.ToString());

        //显卡名称

        Debug.LogError("显卡名称" + SystemInfo.graphicsDeviceName);

        //显卡类型

        Debug.LogError("显卡类型" + SystemInfo.graphicsDeviceType.ToString());

        //显卡供应商

        Debug.LogError("显卡供应商" + SystemInfo.graphicsDeviceVendor);

        //显卡供应唯一ID

        Debug.LogError("显卡供应唯一ID" + SystemInfo.graphicsDeviceVendorID.ToString());

        //显卡版本号

        Debug.LogError("显卡版本号" + SystemInfo.graphicsDeviceVersion);

        //显卡内存大小

        Debug.LogError("显存大小MB" + SystemInfo.graphicsMemorySize.ToString());

        //显卡是否支持多线程渲染

        Debug.LogError("显卡是否支持多线程渲染" + SystemInfo.graphicsMultiThreaded.ToString());

        //支持的渲染目标数量

        Debug.LogError("支持的渲染目标数量" + SystemInfo.supportedRenderTargetCount.ToString());*/
    }

    private void Close()
    {
        StartCoroutine(ClosePanel());
    }

    IEnumerator ClosePanel()
    {
        logingPanel.DOLocalMoveX(750, timeMove);
        yield return new WaitForSeconds(timeMove);
        gameObject.SetActive(false);
    }
    private void CloseMessage()
    {
        messageTF.DOLocalMoveX(750, timeMove);
    }

    void Send()
    {
        Debug.LogError(input_iphone.text + input_pass.text);
        StartCoroutine(ClosePanel());
        if (!string.IsNullOrEmpty(input_iphone.text) && !string.IsNullOrEmpty(input_pass.text))
            ResoureManager.Instance.Send(input_iphone.text, input_pass.text);
    }
    /// <summary>
    /// 显示 term
    /// </summary>
    private void ShowTerm()
    {
        messageTF.DOLocalMoveX(0, timeMove);
    }

    private void OnEnable()
    {
        logingPanel.DOLocalMoveX(0, timeMove);
    
    }
}
