using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum FingerIE
{
    zero,
    OneFinger,
    TwoFinger,
}
public class FingerController : MonoBehaviour
{

    public Vector3 initialRot;
    public Vector3 initialSca;
    public static FingerController instance;
    IEnumerator ie;
    FingerIE finger_num = FingerIE.zero;
    public Camera camera;
    public Camera world;
    void Awake()
    {
        instance = this;
    }
    public bool ismove = true;
    // Update is called once per frame
    void Update()
    {


      
           
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                Debug.Log("click object name is ---->" + Input.mousePosition);
                return;
            }
          

           // RaycastHit2D hit = Physics2D.Raycast(world.transform.position,-world.ScreenToWorldPoint(Input.mousePosition));
           //Debug.DrawRay(world.transform.position,-world.ScreenToWorldPoint(Input.mousePosition), Color.red);
           // if (hit.collider != null)
           // {
           //     Debug.Log("click object name is ---->" + hit.collider.name);
           //     return;
           // }
        }

        if (Input.touchCount == 0)
        {
            if (finger_num != FingerIE.zero)
            {
                StopCoroutine(ie);
                ie = null;
                finger_num = FingerIE.zero;
            }
        }
        else if (Input.touchCount == 1)
        {

            //RaycastHit2D hit = Physics2D.Raycast(camera.transform.position,Input.GetTouch(0).position);
            
            //if (hit.collider!=null)
            //{
            //    Debug.Log("click object name is ---->" +hit.collider.name);
            //    return;
            //}
            if (finger_num != FingerIE.OneFinger)
            {
                if (ie != null)
                {
                    StopCoroutine(ie);
                }
                ie = IMonitorMouseOneFinger();
                StartCoroutine(ie);
                finger_num = FingerIE.OneFinger;
            }
        }
        else if (Input.touchCount == 2)
        {
            if (finger_num != FingerIE.TwoFinger)
            {
                if (ie != null)
                {
                    StopCoroutine(ie);
                }
                ie = IIMonitorMouseTwoFinger();
                StartCoroutine(ie);
                finger_num = FingerIE.TwoFinger;
            }
        }

    }
    float movex = 0;
    float movey = 0;

    /// <summary>
    /// 一根手指控制转动
    /// </summary>
    /// <returns></returns>
    IEnumerator IMonitorMouseOneFinger()
    {

        Touch oneFingerTouch;
        while (true)
        {
            oneFingerTouch = Input.GetTouch(0);
            if (oneFingerTouch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPos = oneFingerTouch.deltaPosition;
                movex += (-Vector3.left * deltaPos.y * 0.2f).x;
                if (movex >= 15)
                {
                    movex = 15;
                }
                else if (movex <= -15)
                {
                    movex = -15;
                }
                movey += (-Vector3.up * deltaPos.x * 0.2f).y;

                transform.rotation = Quaternion.Euler(movex, movey, transform.rotation.z);
                // transform.Rotate(-Vector3.up * deltaPos.x * 0.2f, Space.World);
                // movey = -Vector3.up * deltaPos.x * 0.2f
                //if (transform.rotation.x <= 15 && transform.rotation.x >= -15)
                //    transform.Rotate(-Vector3.left * deltaPos.y * 0.2f, Space.World);
            }
            yield return 0;
        }
    }
    /// <summary>
    /// 两个手指控制缩放
    /// </summary>
    /// <returns></returns>
    IEnumerator IIMonitorMouseTwoFinger()
    {
        Touch firstOldTouch;
        Touch secondOldTouch;
        Touch firstNewTouch;
        Touch secondNewTouch;
        float oldDistance;
        float newDistance;
        while (true)
        {
            firstOldTouch = Input.GetTouch(0);
            secondOldTouch = Input.GetTouch(1);
            oldDistance = Vector2.Distance(firstOldTouch.position, secondOldTouch.position);
            yield return 0;
            firstNewTouch = Input.GetTouch(0);
            secondNewTouch = Input.GetTouch(1);
            newDistance = Vector2.Distance(firstNewTouch.position, secondNewTouch.position);
            if (oldDistance > newDistance && camera.fieldOfView <= 90)
            {
                camera.fieldOfView += 1;
            }
            else if (oldDistance < newDistance && camera.fieldOfView >= 30)
            {
                camera.fieldOfView -= 1;
            }
            //if (oldDistance > newDistance && this.transform.localScale.x > 0.25f)
            //{
            //    this.transform.localScale -= Vector3.one * 0.1f;

            //}
            //else if (oldDistance < newDistance && this.transform.localScale.x < 2f)
            //{
            //    this.transform.localScale += Vector3.one * 0.1f;
            //}
        }
    }
    /// <summary>
    /// 复位
    /// </summary>
    public void ResetRot()
    {
        //this.transform.localEulerAngles = initialRot;
        //this.transform.localScale = initialSca;
        movex = 0;
        movey = 0;
    }
}