using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vuforia;





public class ARPanel : MonoBehaviour
{

    public GameObject Shaomiao;

    public GameObject MessageObj;

    public OnTrackingFound OnTrackingFound;

    public Transform butTf;
    private List<Button> butList = new List<Button>();
    private List<GameObject> butShowObj = new List<GameObject>();
    public List<GameObject> messagePanelObj = new List<GameObject>();
    public Text texName;
    public Text texInfo;
    public string pinchStr = "\u3000\u3000";


    public Transform topTf;
    private List<Button> topButList = new List<Button>();
    private List<GameObject> topShowLost = new List<GameObject>();

    public Button CloseBut;
    public Transform modelParent;
    public void Start_1()
    {
        Shaomiao.SetActive(true);
        MessageObj.SetActive(false);
        CloseMessageObj();

        OnTrackingFound += TrackingFound;
        Button temp;
        GameObject go;
        for (int i = 0; i < butTf.childCount; i++)
        {
            temp = butTf.GetChild(i).GetComponent<Button>();
            butList.Add(temp);
            int k = i;
            temp.onClick.AddListener(delegate ()
            {
                ButOnClick(k);
            });

            go = TransfromHelp.Find(temp.transform, "Image").gameObject;
            butShowObj.Add(go);
            // go.SetActive(false);
        }
        CloseButShow();
        for (int i = 0; i < topTf.childCount; i++)
        {
            temp = topTf.GetChild(i).GetComponent<Button>();
            topButList.Add(temp);
            int num = i;
            temp.onClick.AddListener(delegate ()
            {
                TopOnClick(num);
            });


            go = TransfromHelp.Find(temp.transform, "Image").gameObject;
            topShowLost.Add(go);
            go.SetActive(false);
        }
        CloseBut.onClick.AddListener(delegate ()
        {
            TransfromHelp.OpenScene(1);
        });

    }
    Transform Tempmodel;
    Transform Tempparent;
    private void TrackingFound(Transform model, Transform parent)
    {
        Shaomiao.SetActive(false);
        MessageObj.SetActive(true);
        CloseMessageObj();
        CloseButShow();
        if (Tempmodel != null || Tempparent != null)
        {
            Tempmodel.SetParent(Tempparent);
            Tempmodel.gameObject.SetActive(false);
        }
        model.gameObject.SetActive(true);
        Tempmodel = model;
        Tempparent = parent;
        Tempmodel.SetParent(modelParent);
        Tempmodel.localPosition = Vector3.zero;
        Tempmodel.localRotation = Quaternion.identity;
        Tempmodel.localScale = Vector3.one;
        SetText(parent.name);
    }

    void SetText(string info)
    {
        int id = -1;
        if (ResoureManager.Instance.InfoIdList.TryGetValue(info, out id))
        {
            InfoCode code;
            if (ResoureManager.Instance.m_InfoList.TryGetValue(id, out code))
            {
                texName.text = code.name;
                texInfo.text = pinchStr + code.info;
            }
        }
    }

    /// <summary>
    /// 按钮隐藏
    /// </summary>
    void CloseButShow()
    {
        for (int i = 0; i < butShowObj.Count; i++)
        {
            butShowObj[i].SetActive(false);
        }
    }

    /// <summary>
    /// find model
    /// </summary>
    void CloseMessageObj()
    {
        for (int i = 0; i < messagePanelObj.Count; i++)
        {
            messagePanelObj[i].SetActive(false);
        }
    }

    private void ButOnClick(int k)
    {
        ChangeBut(k);
    }

    /// <summary>
    /// botton  按钮
    /// </summary>
    /// <param name="k"></param>
    private void ChangeBut(int k)
    {
        bool bo = butShowObj[k].activeSelf;
        CloseMessageObj();
        for (int i = 0; i < butShowObj.Count; i++)
        {
            butShowObj[i].SetActive(false);
        }
        if (k != 2)
        {
            messagePanelObj[k].SetActive(!bo);
            butShowObj[k].SetActive(!bo);
        }
       
    

        switch (k)
        {
            case 0:
                TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
                if (Tempmodel != null)
                    Tempmodel.gameObject.SetActive(true);
                break;
            case 1:

                if(!bo)
                {
                    TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
                    if (Tempmodel != null)
                        Tempmodel.gameObject.SetActive(false);
                }
                else
                {
                    TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
                    if (Tempmodel != null)
                        Tempmodel.gameObject.SetActive(true);
                }
                break;
            case 2:
                if (Tempmodel != null)
                {
                    Tempmodel.gameObject.SetActive(true);
                    AinmatorContraller contraller = Tempmodel.GetComponentInChildren<AinmatorContraller>();
                    if (contraller != null)
                        contraller.PlayAnimator();
                }
                TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
                break;
        }
    }
  
    private void PlayAnimator()
    {
        CloseMessageObj();

        if (Tempmodel != null)
        {
            Tempmodel.gameObject.SetActive(true);
            AinmatorContraller contraller = Tempmodel.GetComponentInChildren<AinmatorContraller>();
            if (contraller != null)
                contraller.PlayAnimator();
        }
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
    }


    private void OnDisable()
    {
        OnTrackingFound -= TrackingFound;
    }

    /// <summary>
    /// 分享
    /// </summary>
    /// <param name="index"></param>
    void TopOnClick(int index)
    {
        bool bo = topShowLost[index].activeSelf;
        topShowLost[index].SetActive(!bo);

        switch (index)
        {
            case 0:
                Gloab.IsActive = !bo;
                break;
            case 1:

                break;
            case 2:

                break;
        }
    }

}
