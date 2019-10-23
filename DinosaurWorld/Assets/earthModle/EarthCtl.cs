using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthCtl : MonoBehaviour
{

    /// <summary>
    /// 地球控制
    /// </summary>
    // Use this for initialization

    Transform earth;
    bool isAuto = true;
    float time = 0;
    //public List<Transform> list;
    void Awake()
    {
        earth = this.transform;
        //for (int i = 0; i < list.Count; i++)
        //    list[i].gameObject.SetActive(false);
        gameObject.SetActive(false);

#if UNITY_ANDRIOD
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
#elif UNITY_IOS
        transform.localScale = Vector3.one;
#endif
    }
    private void OnEnable()
    {
        isAuto = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            isAuto = false;
            time = 0;
            StartCoroutine(IMonitorMouseOneFinger());
        }
        else
        {
            if (!isAuto)
            {
                time += Time.deltaTime;
                if (time > 5)
                {
                    isAuto = true;
                }
            }
        }
    }

    //public void SetActive(string model)
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        if (list[i].name == model)
    //            list[i].gameObject.SetActive(true);
    //        else
    //            list[i].gameObject.SetActive(false);
    //    }
    //}

    void FixedUpdate()
    {
        if (isAuto)
            earth.Rotate(0, 0, Time.deltaTime * 10);

    }


    IEnumerator IMonitorMouseOneFinger()
    {
        Touch oneFingerTouch;
        while (true)
        {
            oneFingerTouch = Input.GetTouch(0);
            if (oneFingerTouch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPos = oneFingerTouch.deltaPosition;
                earth.Rotate(-Vector3.up * deltaPos.x * 0.02f, Space.World);
                // transform.Rotate(-Vector3.left * deltaPos.y * 0.2f, Space.World);
            }
            yield return 0;
        }
    }
}
