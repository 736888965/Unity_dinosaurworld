using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AinmatorContraller : MonoBehaviour
{

    private Animator animator;
    public bool IsAnimator = true;
    private Camera camera;
    public Music music;
    void Awake()
    {
        animator = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
    }

    public int index = 0;
    public int count = 1;
    private bool beginDJ = true;
    void Update_1()
    {
        if (beginDJ && Input.GetMouseButtonDown(0))
        {//判断是否是点击事件
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Debug.Log("$$$$$$$$$$$$$");
                animator.SetBool(index.ToString(), true);
                index++;
                index %= count;
                AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
                //for (int i = 0; i < clips.Length; i++)
                //{
                //    Debug.Log(clips[i].length + "*******" + clips[i].name);
                //}

                //如果是一根手指触摸屏幕而且是刚开始触摸屏幕
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (Input.GetTouch(0).tapCount == 1)
                    {//判断点击的次数
                        animator.SetBool(index.ToString(), true);
                        index++;
                        index %= count;
                    }
                }
            }
        }
    }


    private void OnEnable()
    {
        if (IsAnimator && animator != null)
            animator.Play("Idle");
    }
    private void Update()
    {
        if (IsAnimator&&Input.GetMouseButtonDown(0) && beginDJ)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == 8)
                {
                    PlayAnimator();
                }
            }
        }
    }

    public void PlayAnimator()
    {
        if (!beginDJ||!IsAnimator)
            return;
        beginDJ = false;
        animator.SetBool(index.ToString(), true);
        index++;
        index %= count;
        //StartCoroutine(Stop());
    }

    void StopAnim()
    {
        if (!IsAnimator||animator==null||!animator.isActiveAndEnabled)
            return;
        //Debug.Log(gameObject.name + "########stop");
        for (int i = 0; i < count; i++)
        {
            animator.SetBool(i.ToString(), false);
        }
        beginDJ = true;
    }

    public void Play()
    {

        if (!IsAnimator)
            return;
        //AudioSourceManager.Instance.OpenModelAnimation("Music/" + gameObject.name + "/1");
        Debug.Log(gameObject.name);
        Transform tempOBJ = gameObject.transform.parent.parent;
        Debug.Log(tempOBJ.name);
        music.SetModel("Music/model/"+ tempOBJ.name);
    }


    private void OnDisable()
    {
        if (!IsAnimator)
            return;
        StopAnim();
    }

}
