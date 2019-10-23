using System.Collections.Generic;
using UnityEngine;
  public  class UITool
    {

    public static Transform SpawnUI(string path)
    {
        Transform source = Resources.Load<Transform>(path);
        UILayer layer = source.GetComponent<AUIBase>().Getlayer();
        Transform parent = UILayerManager.Instance.UILayerObjDic[layer].transform;
        return SpawnObject(source, parent);
    }

    public static Transform SpawnObject(Transform source,Transform parent)
    {
        if (source != null)
            return Object.Instantiate(source, parent);
        else
        {
            Debug.LogError("source is null");
            return null;
        }
    }
    }

