using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AUIEffect : MonoBehaviour
{

    private RectTransform rectTrans;

    protected RectTransform RectTrans
    {
        get
        {
            if (rectTrans == null)
                rectTrans = GetComponent<RectTransform>();
            return rectTrans;
        }
    }
    /// <summary>
    /// get screen width
    /// </summary>
    protected float DefeultScreenWidth
    {
        get { return FindObjectOfType<CanvasScaler>().referenceResolution.x; }
    }
    /// <summary>
    ///  normal  show  anchorPosition
    /// </summary>
    protected Vector2 DefaultAnchorPos { get; set; }

    /// <summary>
    /// UI 在AnchorPostition在 X 轴上的偏移 从offset到0
    /// </summary>
    protected float offset;
    /// <summary>
    /// add ui offset onenable event
    /// </summary>
    protected Action onEnterComplete;
    /// <summary>
    /// add ui offset ondisable event
    /// </summary>
    protected Action OnExitComplete;

    /// <summary>
    /// ui offset state flag
    /// </summary>
    public UIShowState uiShowState = UIShowState.Default;

    /// <summary>
    /// ui offset onenable event
    /// </summary>
    public abstract void Enter();
    /// <summary>
    /// ui offset ondisable event
    /// </summary>
    public abstract void Exit();

    public virtual void AddEnterListener(Action action)
    {
        onEnterComplete = action;
    }

    public virtual void AddExitListener(Action action)
    {
        OnExitComplete = action;
    }
}

public static class UIEffectTime
{
    public const float SLIDE_FROM_LEFT = 0.5f;
    public const float SLIDE_FROM_Right = 0.5f;
    public const float OPEN_FROM_MIDDLE = 0.5f;
    public const float FROM_LEFT_PULLED = 1f;
    public const float POP_FROM_UI = 0.6f;
}

public enum UIShowState
{
    Default,
    New,
    Old,
}

