using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Xml;
using System;
using System.IO;

public class VuforiaSCDAR : MonoBehaviour
{

    DataSet m_Dataset = null;
    ObjectTracker tracker;

    string xmlPath;

    int ImageTargetCount;
    //寄存
    List<string> ModelName = new List<string>();
    string loadPath;
    void Start()
    {
        ImageTargetCount = 0;

        loadPath = Application.persistentDataPath + "/Scenes/AR";
        xmlPath = Application.streamingAssetsPath + "/QCAR/Dinosaur";
        Debug.Log(loadPath);
        //VuforiaARController.Instance.RegisterVuforiaStartedCallback(VuforiaStaredtCallBack);
        StartCoroutine(GetXML(xmlPath));
        //LoadModel();
      

    }

    private void VuforiaStaredtCallBack()
    {
       // initDone = true;

        StartCoroutine(GetXML(xmlPath));
    }

    /// <summary>
    /// 解析XML
    /// </summary>
    /// <param name="path">XML地址</param>
    IEnumerator GetXML(string path)
    {
        WWW www = new WWW(path+".dat");
        yield return www;
        Debug.Log(www.error + "###############");

        string str = Path.GetDirectoryName(loadPath);
        if(!Directory.Exists(str))
        {
            Directory.CreateDirectory(str);
        }
        File.WriteAllBytes(loadPath + ".dat", www.bytes);
        www = new WWW(path + ".xml");
        yield return www;
        File.WriteAllBytes(loadPath + ".xml", www.bytes);
        str = www.text;
        Debug.Log("XXXXMMMLLL  " + str);
        int _ImageTargetCount = 0;

        XmlDocument xmlDoc = new XmlDocument();
        // xmlDoc.Load(path);
        xmlDoc.LoadXml(str);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("QCARConfig").ChildNodes;
        foreach (XmlElement xe in nodeList)
        {
            Debug.Log("NAME :" + xe.Name);
            foreach (XmlElement x1 in xe.ChildNodes)
            {
                if (x1.Name == "ImageTarget")
                {
                    _ImageTargetCount++;
                    Debug.Log("VALUE :" + x1.GetAttribute("name"));
                    ModelName.Add(x1.GetAttribute("name"));
                }
            }
        }
        Debug.Log(_ImageTargetCount+"!!!!!!!!!!!!!!"+ModelName.Count);
        ImageTargetCount = _ImageTargetCount;
        initDone = true;
    }
    public static bool initDone = false;
    bool Bool_Loaded = false;
    void Update()
    {
        if (initDone)
        {
            if (VuforiaRuntimeUtilities.IsVuforiaEnabled() && !Bool_Loaded)
            {
                if (m_Dataset == null)
                {
                    tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                    m_Dataset = tracker.CreateDataSet();

                }
             
                //TransfromHelp.DisableVuforia();
                tracker.Stop();
                Bool_Loaded = m_Dataset.Load(loadPath+".xml", VuforiaUnity.StorageType.STORAGE_ABSOLUTE);
               // Bool_Loaded = m_Dataset.Load("dinosaur.xml");

                Debug.LogError("错误 ："+Bool_Loaded);
                //TransfromHelp.DisableVuforia();
                tracker.ActivateDataSet(m_Dataset);
                tracker.Start();
                //TransfromHelp.EnableVuforia();


                int i = ImageTargetCount;
                foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
                {

                    if (go.name == "New Game Object")
                    {
                        go.name = /*"ImageTarget_" + */ModelName[i - 1];
                        go.AddComponent<TurnOffBehaviour>();
                        go.AddComponent<DefaultTrackableEventHandler>();
                        //go.AddComponent<FindSon>();
                        i--;

                    }
                }
            }
        }

    }
}



