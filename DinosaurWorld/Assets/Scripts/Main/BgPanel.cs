using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BgPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public RectTransform rect;
    private float width;
    //public Transform pos;
    public List<Transform> listPos = new List<Transform>();
    public Transform tfParrent;
    public List<Transform> objList = new List<Transform>();
    public Transform tempTf;
    private float tempTfY;



    private float beginPos;
    private float endPos;
    private int index = 1;
    public Vector3 targetPos;
    private Vector3 tempPos;

    private bool isDragMove = false;

    private bool autoMove = false;

    private bool handMove = false;

    Coroutine autoCor;

    public Camera mainCamera;


    public Transform logoTf;
    private List<GameObject> logoList = new List<GameObject>();
    void Start()
    {
        tempTfY = tempTf.localPosition.y;
        for (int i = 0; i < rect.childCount; i++)
        {
            listPos.Add(rect.GetChild(i));
        }
        for (int i = 0; i < tfParrent.childCount; i++)
        {
            objList.Add(tfParrent.GetChild(i));
            tfParrent.GetChild(i).position = listPos[i].position;
        }
        tempPos = tempTf.position;
        width = Mathf.Abs(listPos[0].position.x - listPos[1].position.x);

        for (int i = 0; i < logoTf.childCount; i++)
        {
            logoList.Add(logoTf.GetChild(i).gameObject);
            if (i != 1)
                logoTf.GetChild(i).gameObject.SetActive(false);
        }
        //autoCor = StartCoroutine(ControlAutoMove());

    }
    void OnEnable()
    {
        autoCor = StartCoroutine(ControlAutoMove());
    }
    IEnumerator ControlAutoMove()
    {
        yield return new WaitForSeconds(3);
        index++;
        if (index < 0) index = 2;
        index %= 3;
        targetPos = listPos[0].position;
        SetParent();
        isDragMove = true;
        autoCor = StartCoroutine(MoveUpadate());
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (autoMove || isDragMove) return;
        handMove = true;
        if (autoCor != null) StopCoroutine(autoCor);
        beginPos = eventData.position.x;

        SetParent();
    }
    void SetParent()
    {
        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].SetParent(tempTf);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (autoMove || isDragMove) return;
        if (!handMove) return;
        float vec = 2 * (eventData.position.x - beginPos);
        float x = mainCamera.WorldToViewportPoint(new Vector3(vec, eventData.position.y, 0)).x;
        //Debug.Log(width + "#######" + x);
        tempTf.localPosition = new Vector2(x, tempTfY);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (autoMove || isDragMove) return;
        if (!handMove) return;
        endPos = eventData.position.x;
        Debug.Log(endPos);
        bool ismove = Mathf.Abs(endPos - beginPos) > Screen.width * 0.3;
        bool isMoveX = endPos > beginPos;
        Debug.Log("WWWWW" + index);
        if (ismove)
        {
            targetPos = isMoveX ? listPos[2].position : listPos[0].position;
            index = isMoveX ? index - 1 : index + 1;
            index %= 3;

            //List<Transform> tt = new List<Transform>();
            //Transform temp;
            //if (isMoveX)
            //{
            //    temp = objList[0];
            //    obj
            //}


        }
        else
        {
            targetPos = listPos[1].position;
        }
        if (index < 0) index = 2;
        Debug.Log("WWWWW" + index);
        isDragMove = true;
        handMove = false;
        autoCor = StartCoroutine(MoveUpadate());
    }



    private void Update_1()
    {
        if (isDragMove || autoMove)
        {
            tempTf.position = Vector3.Lerp(tempTf.position, targetPos, 5 * Time.deltaTime);
            if (Vector3.Distance(targetPos, tempTf.position) <= 0.1f)
            {
                tempTf.position = targetPos;
                //Debug.Log(tempTf.position);
                ChangePos();
                ChangeLogo();
                isDragMove = false;
                autoMove = false;
                //handMove = false;
                autoCor = StartCoroutine(ControlAutoMove());
            }
        }
    }
    IEnumerator MoveUpadate()
    {
        tempTf.DOMoveX(targetPos.x, 1);
        yield return new WaitForSeconds(1);
        tempTf.position = targetPos;
        ChangePos();
        ChangeLogo();
        isDragMove = false;
        autoMove = false;
        autoCor = StartCoroutine(ControlAutoMove());
    }


    private void ChangePos()
    {
        // Debug.Log(index);
        switch (index)
        {
            case 0:
                objList[2].position = listPos[0].position;
                objList[1].position = listPos[2].position;
                objList[0].position = listPos[1].position;
                break;
            case 1:
                objList[0].position = listPos[0].position;
                objList[2].position = listPos[2].position;
                objList[1].position = listPos[1].position;
                break;
            case 2:
                objList[0].position = listPos[2].position;
                objList[1].position = listPos[0].position;
                objList[2].position = listPos[1].position;
                break;
        }
        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].SetParent(tfParrent);
        }
        tempTf.position = tempPos;
    }

    void ChangeLogo()
    {
        for (int i = 0; i < logoList.Count; i++)
        {
            if (i == index)
                logoList[i].SetActive(true);
            else
                logoList[i].SetActive(false);
        }
    }


    public void Open()
    {
        autoCor = StartCoroutine(ControlAutoMove());
    }

    public void Close()
    {
        tempTf.position = targetPos;
        ChangePos();
        ChangeLogo();
        isDragMove = false;
        autoMove = false;
        handMove = true;
        if (autoCor != null)
            StopCoroutine(autoCor);
    }

    void OnDisable()
    {
        index = 1;
        //ChangePos();
        Close();
    }
}