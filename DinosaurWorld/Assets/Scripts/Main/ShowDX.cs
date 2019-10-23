using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDX : MonoBehaviour {


    public List<GameObject> list = new List<GameObject>();

    public void Show(int index)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
                list[index].SetActive(true);
            else
                list[i].SetActive(false);
        }
        Debug.Log("##################################");
    }


    //private void OnDisable()
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        list[i].SetActive(false);
    //    }
    //}
}
