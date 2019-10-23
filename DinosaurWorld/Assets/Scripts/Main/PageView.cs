using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageView : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{

    private ScrollRect rect;
    private float targethorizontal = 0;

    private bool isDrag = false;

    private List<float> posList = new List<float>();

    private int currentPageIndex = -1;

    private Action<int> OnPageChanged;
    public RectTransform content;
    private bool stopMove = true;
    public float smooting = 4;
    public float sensitivity = 0;
    private float startTime;

    private float startDragHorizontal;
    public Transform toggleList;

    private void Start()
    {
        rect = transform.GetComponent<ScrollRect>();
        RectTransform rectWith = transform.GetComponent<RectTransform>();
        float width = ((float)content.transform.childCount * rectWith.rect.width);
        content.sizeDelta = new Vector2(width, rectWith.rect.height);
        float horizontalLength = content.rect.width - rectWith.rect.width;
        for (int i = 0; i < rect.content.transform.childCount; i++)
        {
            posList.Add(rectWith.rect.width * i / horizontalLength);
        }
    }

    private void Update()
    {
        if(!isDrag&&!stopMove)
        {
            startTime += Time.deltaTime;
            float t = startTime * smooting;
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition,targethorizontal, t);
            if (t >= 1)
                stopMove = true;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        startDragHorizontal = rect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float posX = rect.horizontalNormalizedPosition;
        posX +=((posX-startDragHorizontal)*sensitivity);
        posX = posX < 1 ? posX : 1;
        posX = posX > 0 ? posX : 0;
        int index = 0;
        float offset = Mathf.Abs(posList[index] - posX);


        for (int i = 0; i < posList.Count; i++)
        {
            float temp = Mathf.Abs(posList[i] - posX);
            if(temp<offset)
            {
                index = i;
                offset = temp;
            }
        }
        print(index);
        SetPageIndex(index);
        GetIndex(index);
        targethorizontal = posList[index];
        isDrag = false;
        startTime = 0;
        stopMove = false;
    }


    private void SetPageIndex(int index)
    {
        if(currentPageIndex!=index)
        {
            currentPageIndex = index;
            if (OnPageChanged != null)
                OnPageChanged(index);
        }
    }

    private void PageTo(int index)
    {
        if(index>=0&&index<posList.Count)
        {
            rect.horizontalNormalizedPosition = posList[index];
            SetPageIndex(index);

        }
    }

    public void GetIndex(int index)
    {
        var toogle = toggleList.GetChild(index).GetComponent<Toggle>();
        toogle.isOn = true;
    }
}
