using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class MainPanel : MonoBehaviour
{
    public BgPanel bg;
    public RectTransform peopleRect;

    public Transform butObj;

    public float timeMove = 0.5f;
    private List<Transform> butList = new List<Transform>();

    private List<GameObject> butImg = new List<GameObject>();
    public GameObject mainobj;
    public GameObject canvasObj;
    public Button sendPanel;
    public GameObject loging;
    public Text iphoneText;

    // Use this for initialization
    void Start()
    {
        Transform tf;
        for (int i = 0; i < butObj.childCount; i++)
        {
            butList.Add(butObj.GetChild(i));
            tf = TransfromHelp.Find(butObj.GetChild(i), "Image");
            butImg.Add(tf.gameObject);
            if (i != 0)
                tf.gameObject.SetActive(false);
            int num = i;
            butObj.GetChild(i).GetComponent<Button>().onClick.AddListener(
                delegate ()
                {
                    OnClickButton(num);
                });
        }
        peopleRect.gameObject.SetActive(false);
        mainobj.SetActive(false);
        canvasObj.SetActive(false);
        //TransfromHelp.DisableVuforia();RegisterVuforiaStartedCallback
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        sendPanel.onClick.AddListener(SnedPanelActive);
    }

    private void OnEnable()
    {
        if (SaveData.IsLogin())
        {
            iphoneText.text = SaveData.GetAccount();
            sendPanel.gameObject.SetActive(false);
        }
    }
    private void CloseVideo()
    {
        Debug.LogError("%%%%%%%%%%%%%%");
        TransfromHelp.DisableVideo();

    }

    private void OnVuforiaStarted()
    {
        TransfromHelp.DisableVuforia();
        //TransfromHelp.DisableVideo();
    }

    private void OnClickButton(int index)
    {
        switch (index)
        {
            case 0:
                if (peopleRect.gameObject.activeSelf)
                {
                    CloseBg();
                    OpenBg(index);
                    StartCoroutine(ClosePeople());
                }
                break;
            case 1:
                //Transform tf;
                //for (int i = 0; i < butObj.childCount; i++)
                //{
                //    butList.Add(butObj.GetChild(i));
                //    tf = TransfromHelp.Find(butObj.GetChild(i), "Image");
                //    if (i != 0)
                //        tf.gameObject.SetActive(false);
                //}


                TransfromHelp.EnableVuforia();
                TransfromHelp.EnableVideo();
                mainobj.SetActive(true);
                canvasObj.SetActive(true);
                gameObject.SetActive(false);
                break;
            case 2:
                CloseBg();
                OpenBg(index);
                StartCoroutine(OpenPeople());
                break;

        }
    }

    IEnumerator OpenPeople()
    {
        peopleRect.gameObject.SetActive(true);
        peopleRect.DOLocalMoveX(0, timeMove);
        yield return new WaitForSeconds(timeMove);
        bg.Close();
    }
    IEnumerator ClosePeople()
    {
        peopleRect.DOLocalMoveX(800, timeMove);
        yield return new WaitForSeconds(timeMove);
        peopleRect.gameObject.SetActive(false);
        bg.Open();
    }

    void CloseBg()
    {
        for (int i = 0; i < butImg.Count; i++)
        {
            butImg[i].SetActive(false);
        }
    }
    void OpenBg(int index)
    {
        if (index < butImg.Count)
            butImg[index].SetActive(true);
    }


    void SnedPanelActive()
    {
        loging.SetActive(true);
    }
}
