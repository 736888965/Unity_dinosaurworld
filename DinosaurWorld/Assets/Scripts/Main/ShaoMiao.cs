using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaoMiao : MonoBehaviour
{
    public RectTransform rect;
    private Vector3 pos;
    public float time = 2;
    private void Awake()
    {
        pos = rect.position;
    }
    private void OnEnable()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        rect.DOLocalMoveY(-750, time);
        yield return new WaitForSeconds(time);
        rect.position = pos;
        StartCoroutine(Move());
    }
}
