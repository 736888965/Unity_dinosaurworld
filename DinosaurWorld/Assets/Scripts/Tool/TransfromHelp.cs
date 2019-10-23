using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

public class TransfromHelp
{

    public static Transform Find(Transform tf, string name)
    {
        if (tf.name == name)
            return tf;
        Transform child = tf.Find(name);
        if (child != null)
            return child;

        Transform go = null;
        for (int i = 0; i < tf.childCount; i++)
        {
            child = tf.GetChild(i);
            go = Find(child, name);
            if (go != null)
                return go;
        }
        return null;
    }
    public static T Find<T>(Transform tf, string name) where T : Object
    {
        Transform child = tf.Find(name);
        if (child != null)
        {
            return child.GetComponent<T>();
        }

        Transform go = null;
        for (int i = 0; i < tf.childCount; i++)
        {
            child = tf.GetChild(i);
            go = Find(child, name);
            if (go != null)
            {
                return go.GetComponent<T>();
            }
        }
        return null;
    }

    public static void AddBtnListener(Transform parent, UnityAction action, string buttonName = "")
    {
        if (parent == null || action == null)
        {
            Debug.LogError("The parameter 'parent' or 'action' of the UITool.AddBtnListener method cannot be null");
            return;
        }
        if (!string.IsNullOrEmpty(buttonName))
        {
            Button m_Button = Find<Button>(parent, buttonName);
            if (m_Button != null)
                m_Button.onClick.AddListener(() => { action(); });
            else
                Debug.LogError("on the " + m_Button.name + " cannot find button component");
        }
        else
        {
            Debug.LogError("on the " + parent.name + " cannot find button component");
        }
    }


    /// <summary>
    /// 关闭vuforia识别
    /// </summary>
    public static void DisableVuforia()
    {
        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();

    }

    public static void DisableVideo()
    {
        VuforiaBehaviour.Instance.enabled = false;
        VideoBackgroundManager.Instance.SetVideoBackgroundEnabled(false);
    }
    public static void EnableVuforia()
    {
        TrackerManager.Instance.GetTracker<ObjectTracker>().Start();

    }
    public static void EnableVideo()
    {
        VuforiaBehaviour.Instance.enabled = true;
        VideoBackgroundManager.Instance.SetVideoBackgroundEnabled(true);
    }

    public static void OpenScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public static GameObject ResourceOBJ(string path)
    {
        return Resources.Load<GameObject>(path);
    }
    public static AudioClip ResourceMusic(string path)
    {
        return Resources.Load<AudioClip>(path);
    }
}
