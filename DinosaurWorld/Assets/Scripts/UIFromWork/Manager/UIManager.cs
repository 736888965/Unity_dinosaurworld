using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : IUIManager
{
    private Func<string, Object, bool> UIInitAction;

    private Func<string, bool, bool> UIActiveAction;

    private Stack<UIHandler> uiStack;

    private Dictionary<string, Transform> prefabPool;

    public UIManager()
    {
        prefabPool = new Dictionary<string, Transform>();
        uiStack = new Stack<UIHandler>();
    }
   
    public void AddUIInitListener(Func<string,Object,bool> action)
    {           
        UIInitAction = action;
    }
  
    public void AddUIActiveListener(Func<string,bool,bool> action)
    {
        UIActiveAction = action;
    }

    public virtual void ShowUI<T>(T id)
    {
        Transform uiTrans = SpawnUI(id.ToString());
        AUIBase ui = uiTrans.GetComponent<AUIBase>();
        if (ui == null)
            throw new Exception("can't find Auibase component");
        if (ui.Getlayer() == UILayer.BasicUI)
        {
            UIHandler newHandler = new UIHandler(ui);
            if (uiStack.Count > 0)
                uiStack.Peek().Hide(ui.Getlayer());
            AddListener(ui, id.ToString(), newHandler);
            uiStack.Push(newHandler);
        }
        else
        {
            AddListener(ui, id.ToString(), uiStack.Peek());
            uiStack.Peek().Show(ui);
        }
    }
    public void Back()
    {
        if(!uiStack.Peek().Back()&&uiStack.Count>1)
        {
            uiStack.Pop().Hide(UILayer.BasicUI);
            UIHandler handler = uiStack.Peek();
            handler.BackToShow();
        }
    }
    public void AddListener(AUIBase ui,string id ,UIHandler handler)
    {
        handler.AddListener(ui, ob => UIInitAction(id.ToString(), ob), isActive => UIActiveAction(id.ToString(), isActive));
    }
    private Transform SpawnUI(string id)
    {
        string path = AUIPathManager.GetPath(id);
        if (string.IsNullOrEmpty(path))
        {
            if (!prefabPool.ContainsKey(id) || prefabPool[id] == null)
                prefabPool[id] = UITool.SpawnUI(path);
            return prefabPool[id];
        }
        else
            return null;
    }

  
}


public class UIHandler
{
    private UIData data;

    public BasicUI BasicUI
    {
        get { return data.BasicUI; }
    }

    public UIHandler(AUIBase basicUI)
    {
        if (basicUI != null)
            data = new UIData((BasicUI)basicUI);
        else
            throw new Exception("basicUI is null");
    }
    /// <summary>
    /// show UI
    /// </summary>
    /// <param name="ui"></param>
    public void Show(AUIBase ui)
    {
        switch(ui.Getlayer())
        {
            case UILayer.BasicUI:
                ShowUI<BasicUI>(ui);
                break;
            case UILayer.OverLayUI:
                ShowUI(ui, data.OverlayUIStack);
                break;
            case UILayer.TopUI:
                ShowUI(ui, data.TopUIStack);
                break;
        }
    }


    public void BackToShow()
    {
        ShowUI<BasicUI>(data.BasicUI);
        if (data.OverlayUIStack.Count > 0)
            data.OverlayUIStack.Peek().UIState = UIStateEnum.SHOW;
        if (data.TopUIStack.Count > 0)
            data.TopUIStack.Peek().UIState = UIStateEnum.SHOW;
    }
    public void Hide(UILayer showLayer)
    {
        HideUI<BasicUI>(showLayer, UILayer.BasicUI);
        HideUI(showLayer, UILayer.OverLayUI,data.OverlayUIStack);
        HideUI(showLayer, UILayer.TopUI,data.TopUIStack);

    }

    /// <summary>
    /// 返回上一界面
    /// </summary>
    ///  true代表Overlay或Top层有界面返回成功，
    ///     若为false，代表需要返回的是当前数据类的BasicUI
    /// <returns></returns>
    public bool Back()
    {
        if (CloseUI(data.TopUIStack))
            return true;
        else
        {
            if (CloseUI(data.OverlayUIStack))
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// add ui event
    /// </summary>
    /// <param name="ui"></param>
    /// <param name="initAction"></param>
    /// <param name="activeAction"></param>
    public void AddListener(AUIBase ui,Func<Object,bool> initAction,Func<bool,bool> activeAction)
    {
        if(ui.UIState==UIStateEnum.NOTINIT)
        {
            ui.AddInitListener(initAction);
            ui.AddInitListener(activeAction);
        }
    }

    /// <summary>
    /// show UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ui"></param>
    /// <param name="stack"></param>
    public void ShowUI<T>(AUIBase ui,Stack<T> stack=null) where T:AUIBase
    {
        if(stack!=null)
        {
            if (stack.Count > 0)
                stack.Peek().UIState = UIStateEnum.HIDE;
            stack.Push((T)ui);
        }
        ui.UIState = UIStateEnum.SHOW;
    }

    /// <summary>
    /// 隐藏UI的处理方法
    /// 当其他高于此层级UI显示时，隐藏UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="showLayer"></param>
    /// <param name="targetLayer"></param>
    /// <param name="stack"></param>
    public void HideUI<T>(UILayer showLayer, UILayer targetLayer, Stack<T> stack = null) where T : AUIBase
    {
        if (showLayer <= targetLayer)
        {
            if (stack != null)
            {
                if (stack.Count > 0)
                    stack.Peek().UIState = UIStateEnum.HIDE;
            }
            else
                data.BasicUI.UIState = UIStateEnum.HIDE;

        }
    }
    /// <summary>
    /// close UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stack"></param>
    /// <returns></returns>
    public bool CloseUI<T>(Stack<T> stack)where T:AUIBase
    {
        if(stack.Count>0)
        {
            stack.Pop().UIState = UIStateEnum.HIDE;
            return true;
        }
        return false;
    }
}

/// <summary>
/// UI 对象的储存数据类
/// 每一个数据类只有一个Basic对象
/// </summary>
public struct UIData
{
    public BasicUI BasicUI { get; set; }

    public Stack<OverlayUI> OverlayUIStack { get; private set; }

    public Stack<TopUI> TopUIStack { get; set; }

    public UIData(BasicUI basic)
    {
        BasicUI = basic;
        OverlayUIStack = new Stack<OverlayUI>();
        TopUIStack = new Stack<TopUI>();
    }
}

