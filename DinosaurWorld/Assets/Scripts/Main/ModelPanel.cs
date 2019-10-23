using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ModelPanel : MonoBehaviour
{

    public Camera camera;
    public ShowDX show;

    public List<Vector3> listPos = new List<Vector3>();
    public List<Vector3> listRot = new List<Vector3>();
    //-1.393   1.219  1.381
    //-2.456    2.427428  2.476
    //-5.18    4.445022    5.45

    //public GameObject AudioSourceOBj;
    public OnTrackingFound OnTrackingFound;

    /// <summary>
    /// 显示的模型
    /// </summary>
    public Transform modelTemp;
    /// <summary>
    /// ImageTarget parent
    /// </summary>
    private Transform parentTemp;

    public GameObject ar_Image;
    public GameObject find_Obj;

    /// <summary>
    /// 显示 message
    /// </summary>
    public Button but_Message;
    public RectTransform obj_Message;
    /// <summary>
    /// 地球按钮
    /// </summary>
    public Button but_Earth;
    public GameObject obj_Earth;
    //public EarthCtl earthctl;
    public DiBiaoShow dibiaoshow;
    /// <summary>
    /// 播放动画
    /// </summary>
    public Button but_PlayModel;

    public Transform but_Obj;
    public List<Button> butList = new List<Button>();
    private List<GameObject> show_Image = new List<GameObject>();

    public Transform modelParent;
    public Text texName;
    public Text texInfo;
    public RectTransform texRect;
    private string pinchStr = "\u3000\u3000";
    public float timeMove = 0.2f;
    /// <summary>
    /// 地形
    /// </summary>
    public GameObject Mountainou;
    public bool isShowMountainou = false;

    public GameObject play;
    public GameObject stop;

    public Music music;
    public TopRightPanel panel;


    //public FingerController controller;
    void Awake()
    {
        OnTrackingFound += TrackingFound;
        Transform temp;
        for (int i = 0; i < but_Obj.childCount; i++)
        {
            temp = but_Obj.GetChild(i);
            butList.Add(temp.GetComponent<Button>());
            show_Image.Add(TransfromHelp.Find(temp, "Image").gameObject);
            int index = i;
            temp.GetComponent<Button>().onClick.AddListener(delegate ()
            {
                ButOnClick(index);
            });
        }
        CloseShow();
        Mountainou.SetActive(false);
        find_Obj.SetActive(false);
        //obj_Earth.SetActive(false);
        //AudioSourceOBj.SetActive(false);
        InitAudio(true);
    }

    private void OnEnable()
    {
        //TransfromHelp.EnableVuforia();
        Mountainou.SetActive(false);
        isShowMountainou = false;
        ar_Image.SetActive(true);
    }

    private void ButOnClick(int index)
    {
        bool bo = show_Image[index].activeSelf;
        if (index == 2)
        {
            isShowMountainou = !bo;
        }
        // controller.ismove = true;
        CloseShow();
        if (index != 1)
        {
            TransfromHelp.EnableVuforia();
            obj_Earth.SetActive(false);
            if (modelTemp != null)
                modelTemp.gameObject.SetActive(true);
            Mountainou.gameObject.SetActive(isShowMountainou);
            show_Image[2].SetActive(isShowMountainou);
            modelParent.gameObject.SetActive(true);
        }
        if (index != 0)
        {
            obj_Message.DOLocalMoveY(-1300, 0);
            //AudioSourceOBj.SetActive(false);
            //AudioSourceManager.Instance.CloseModelMessage();
            //  music.Close();
        }
        music.CloseMessage();
        switch (index)
        {
            case 0:
                if (!bo)
                {
                    texRect.anchoredPosition = Vector3.zero;
                    obj_Message.DOLocalMoveY(-170, timeMove);
                    //  controller.ismove = false;
                    // AudioSourceManager.Instance.OpenModelAnimation("Music/" + modelTemp.name + "/101");
                }
                else
                {
                    obj_Message.DOLocalMoveY(-1300, timeMove);
                }
                //Debug.LogError(!bo + "  index " + index);
                show_Image[0].SetActive(!bo);
                InitAudio(true);
                //AudioSourceOBj.SetActive(!bo);
               // music.SetMessage("Music/" + modelTemp.name + "/101");
                music.SetMessage("Source/" + modelTemp.name);

                break;
            case 1:
                if (!bo)
                {
                    Mountainou.gameObject.SetActive(false);
                    show_Image[2].SetActive(false);
                    modelParent.gameObject.SetActive(false);
                    TransfromHelp.DisableVuforia();
                    //Debug.LogError(!bo + "  index " + index);
                }
                else
                {
                    modelParent.gameObject.SetActive(true);
                    TransfromHelp.EnableVuforia();
                    Mountainou.gameObject.SetActive(isShowMountainou);
                    show_Image[2].SetActive(isShowMountainou);
                    //Debug.LogError(!bo + "  @@@@@@@@@@@@@@@ " + index);
                }
                obj_Earth.SetActive(!bo);
                //earthctl.SetActive(modelTemp.name);
                Debug.LogError("modelTemp.name ; " + modelTemp.name);

                dibiaoshow.ShowDiBiao(modelTemp.name);
                show_Image[1].SetActive(!bo);
                //AudioSourceManager.Instance.Close();
                music.Close();
                if (modelTemp != null)
                    modelTemp.gameObject.SetActive(bo);

                break;
            case 2:
                if (modelTemp != null && !bo)
                    modelTemp.GetComponentInChildren<AinmatorContraller>().PlayAnimator();
                break;
        }

    }

    void InitAudio(bool active)
    {
        play.SetActive(active);
        stop.SetActive(!active);
    }

    public void PlayAudioMessage()
    {
        if (modelTemp != null)
            music.SetMessagePlay();
        InitAudio(false);
    }
    public void StopAudioMessgage()
    {
        music.SetMessagePaure();
        InitAudio(true);
    }



    private void TrackingFound(Transform model, Transform parent)
    {
        InitAudio(true);
        panel.Rest();
        find_Obj.SetActive(true);
        ar_Image.SetActive(false);
        Mountainou.SetActive(false);
        isShowMountainou = false;
        obj_Earth.SetActive(false);
        camera.fieldOfView = 60;
        modelParent.gameObject.SetActive(true);
        modelParent.localPosition = Vector3.zero;
        modelParent.localRotation = Quaternion.identity;
        modelParent.localScale = Vector3.one;
        modelParent.GetComponent<FingerController>().ResetRot();
        //AudioSourceManager.Instance.Close();
        music.Close();

        // 设置 以前的物体
        if (modelTemp != null && parentTemp != null)
        {
            modelTemp.SetParent(parentTemp);
            modelTemp.gameObject.SetActive(false);
            ResoureManager.Instance.ScannOutModel(modelTemp.name);
        }
        CloseShow();
        Debug.LogError(model == null);

        model.gameObject.SetActive(true);
        modelTemp = model;
        parentTemp = parent;
        modelTemp.SetParent(modelParent);
        modelTemp.localPosition = Vector3.zero;
        modelTemp.localRotation = Quaternion.identity;
        modelTemp.localScale = Vector3.one;
        obj_Message.DOLocalMoveY(-1300, 0);
        //Debug.LogError(parent.name + "AAAAAA" + model.name);
        // AudioSourceManager.Instance.SetModelAnimation("Music/" + modelTemp.name + "/101");
        //music.SetMessage("Music/" + modelTemp.name + "/101");
        Debug.LogError(parent.name);
        SetText(parent.name);
       
        //识别
        ResoureManager.Instance.ScannModel(modelTemp.name);
    }
    void SetText(string info)
    {
        int id = -1;
        if (ResoureManager.Instance.InfoIdList.TryGetValue(info, out id))
        {
            Debug.LogError(id);
            InfoCode code;
            if (ResoureManager.Instance.m_InfoList.TryGetValue(id, out code))
            {
                texName.text = code.name;
                texInfo.text = code.info;
            }
        }
        IdName temp = new IdName();
        //Debug.LogError("name : " + ResoureManager.Instance.InfoIdNameList.ContainsKey(info));
        if (ResoureManager.Instance.InfoIdNameList.TryGetValue(info, out temp))
        {
            int index = temp.camera - 1;
            camera.transform.position = listPos[index];
            camera.transform.rotation = Quaternion.Euler(listRot[index]);
            /*
            int count = temp.earth - 1;
            for (int i = 0; i < show.list.Count; i++)
            {
                //Debug.LogError("name : " + show.list[i].name);
                if (i == count)
                    show.list[count].SetActive(true);
                else
                    show.list[i].SetActive(false);
            }
            */
            //show.Show(temp.camera - 1);
        }
    }
    /// <summary>
    /// 关闭显示image
    /// </summary>
    private void CloseShow()
    {
        for (int i = 0; i < show_Image.Count; i++)
        {
            show_Image[i].SetActive(false);
        }
    }

    public void ButMessage()
    {
        bool bo = show_Image[0].activeSelf;
        show_Image[0].SetActive(!bo);
        show_Image[1].SetActive(false);
        float y = bo ? -870 : -170;
        print(y);
        obj_Message.DOLocalMoveY(y, timeMove);
        TransfromHelp.EnableVuforia();
        obj_Earth.SetActive(false);
        if (modelTemp != null)
            modelTemp.gameObject.SetActive(true);
    }
    public void ButEarth()
    {
        bool bo = show_Image[1].activeSelf;
        CloseShow();
        show_Image[1].SetActive(!bo);
        obj_Message.DOLocalMoveY(-870, timeMove);
        if (!bo)
        {
            TransfromHelp.DisableVuforia();
            if (modelTemp != null)
                modelTemp.gameObject.SetActive(true);
        }
        {
            TransfromHelp.EnableVuforia();
        }
        obj_Earth.SetActive(!bo);
        show_Image[1].SetActive(isActive);
    }
    private bool isActive = false;
    public void ButAnimation()
    {
        print("@@@@@@@@@@@@44");

        isActive = !isActive;
        CloseShow();
        show_Image[2].SetActive(isActive);
        obj_Message.DOLocalMoveY(-870, timeMove);
        obj_Earth.SetActive(false);
        if (modelTemp != null && isActive)
            modelTemp.GetComponentInChildren<AinmatorContraller>().PlayAnimator();
    }

    private void OnDisable()
    {
        ar_Image.SetActive(false);
        Mountainou.SetActive(false);
        find_Obj.SetActive(false);
        obj_Earth.SetActive(false);
        CloseShow();
        InitAudio(true);
        if (modelTemp != null && parentTemp != null)
        {
            modelTemp.SetParent(parentTemp);
            modelTemp.gameObject.SetActive(false);
            ResoureManager.Instance.ScannOutModel(modelTemp.name);
        }
    }


}
