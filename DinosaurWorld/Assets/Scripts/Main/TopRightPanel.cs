using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopRightPanel : UIBaseAdapt
{

    private List<GameObject> show_Obj = new List<GameObject>();

    public ModelPanel modelPanel;
    private void Awake()
    {
        Button temp;
        GameObject go;
        for (int i = 0; i < transform.childCount; i++)
        {
            temp = transform.GetChild(i).GetComponent<Button>();
            go = TransfromHelp.Find(temp.transform, "Image").gameObject;
            go.SetActive(false);
            show_Obj.Add(go);
            int index = i; 
            temp.onClick.AddListener(delegate ()
            {
                ButOnClick(index);
            });
        }
    }
  
    public void Rest()
    {

        for (int i = 0; i < show_Obj.Count; i++)
        {
            show_Obj[i].SetActive(false);
        }
        SetState();
    }

    private void SetState()
    {
        if (SaveData.GetSingleState(modelPanel.modelTemp.name))
            show_Obj[0].SetActive(true);
    }


    private void ButOnClick(int index)
    {
       
        bool bo = show_Obj[index].activeSelf;
        show_Obj[index].SetActive(!bo);
        switch (index)
        {
            case 0:
                if(modelPanel.modelTemp!=null)
                {
                    if (!bo)
                        ResoureManager.Instance.Add(modelPanel.modelTemp.name);
                    else
                        ResoureManager.Instance.Delete(modelPanel.modelTemp.name);
                }
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
