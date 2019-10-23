using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{


    public Text txt;

    public Camera camera;

    private void Update_1()
    {
        if (Input.GetMouseButton(0))
        {

            Debug.Log("click object name is ---->");

            RaycastHit2D hit = Physics2D.Raycast(camera.transform.position, Input.mousePosition);

            if (hit.collider != null)
            {
                Debug.Log("click object name is ---->" + hit.collider.name);
                return;
            }
        }

    }
    public Animator animator;
    private void OnGUI()
    {
        if (GUILayout.Button("", GUILayout.Width(100), GUILayout.Height(50)))
        {
            animator.Play("Albertosaurus", -1, -1);
            // animator.StartRecording(0);
            animator.speed = 0;
        }
    }

    IEnumerator LoadWWW()
    {
        WWW www = new WWW("file://"+Application.streamingAssetsPath + "/Configs/Info.md");
        yield return www;

        Debug.Log(www.text);
        InfoList list = JsonUtility.FromJson<InfoList>(www.text);

        foreach (InfoCode item in list.infoList)
        {
            Debug.Log(item.name);
            Debug.Log(item.info);
            txt.text = item.info;
        }
    }
}
