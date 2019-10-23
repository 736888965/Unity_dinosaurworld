using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {

    private void OnGUI()
    {
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            Show(false);
        }
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            Show(true);

        }
    }


    void Show(bool bo)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).gameObject.SetActive(bo);
        }
    }

}
