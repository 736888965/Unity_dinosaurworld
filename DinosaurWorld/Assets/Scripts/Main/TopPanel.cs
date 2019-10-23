using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TopPanel : UIBaseAdapt
{

    public Button closeBut;
    public GameObject mainObj;
    public GameObject canveLoginObj;
    public GameObject canveObj;
    public Music Music;
    private void Start()
    {
        closeBut.onClick.AddListener(delegate ()
        {
            //AudioSourceManager.Instance.Close();
            Music.Close();
            TransfromHelp.DisableVuforia();
            //TransfromHelp.OpenScene(1);
            mainObj.SetActive(false);
            canveObj.SetActive(false);
            canveLoginObj.SetActive(true);
        });
    }

}
