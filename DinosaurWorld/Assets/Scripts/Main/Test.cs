using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{

    AsyncOperation asyn;
    private void Start()
    {

        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        asyn = SceneManager.LoadSceneAsync("1");
        asyn.allowSceneActivation = false;
        yield return asyn;
    }
    private void Update()
    {
        if (asyn != null)
        {
            Debug.Log(asyn.progress);
            if (asyn.progress >= 0.9f)
                asyn.allowSceneActivation = true;
        }
    }


}
