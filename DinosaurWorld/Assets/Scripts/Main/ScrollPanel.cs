using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScrollPanel : MonoBehaviour {

    public ScrollRect rect;

    public Transform parent;


    private void OnEnable()
    {
        Show();
    }
    private void Show()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
        GameObject go = TransfromHelp.ResourceOBJ("Text");
        for (int i = 0; i < ResoureManager.Instance.list.Count; i++)
        {
            GameObject temp = Instantiate(go, parent);
            temp.transform.localScale = Vector3.one;
            temp.transform.localRotation = Quaternion.identity;
            temp.GetComponent<Text>().text = ResoureManager.Instance.GetName(ResoureManager.Instance.list[i]);
        }
        parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * ResoureManager.Instance.list.Count);

        //if (!Gloab.IsActive) return;
       
        //GameObject go = TransfromHelp.ResourceOBJ("Text");
        //Debug.Log(ResoureManager.Instance.m_InfoList.Keys.Count);
        //foreach (var item in ResoureManager.Instance.m_InfoList)
        //{
        //    Debug.Log(item.Key);
        //    GameObject temp = Instantiate(go, parent);
        //    temp.transform.localScale = Vector3.one;
        //    temp.transform.localRotation = Quaternion.identity;
        //    temp.GetComponent<Text>().text = item.Value.name;
        //}
        //parent.GetComponent<RectTransform>().sizeDelta = new Vector2(0,50* ResoureManager.Instance.m_InfoList.Keys.Count);
    }

}
