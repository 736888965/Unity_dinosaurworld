using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class UIBase : AUIBase
{
    /// <summary>
    /// current data dispose
    /// </summary>
    protected IDataHande dataHandler;

    /// <summary>
    /// UI current state
    /// </summary>
    private UIStateEnum uiState;
    /// <summary>
    /// init obj func
    /// </summary>
    private Func<Object, bool> InitAction;
    /// <summary>
    /// show hide func
    /// </summary>
    public Func<bool, bool> ObjectActiveActive;
    /// <summary>
    /// init UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="dataHandlerName"></param>
    protected void InitUI<T>(T id, string dataHandlerName = null)
    {
        ID = id.ToString();
        uiState = UIStateEnum.NOTINIT;
        if (!string.IsNullOrEmpty(dataHandlerName))
        {
            dataHandler = GetDataHandlerManager().GetHandler(dataHandlerName);
            dataHandler.UpdateShow += UpdateShow;

        }
    }
    /// <summary>
    /// set UI state
    /// </summary>
    /// <param name="state"></param>
    protected override void SetUIState(UIStateEnum state)
    {
        HandlerState(uiState, state);
        uiState = state;
    }
    /// <summary>
    /// get ui state
    /// </summary>
    /// <returns></returns>
    protected override UIStateEnum GetUIState()
    {
        return uiState;
    }
    protected override void HandlerState(UIStateEnum currentState, UIStateEnum targetState)
    {
        switch(targetState)
        {
            case UIStateEnum.INIT:
                Init();
                break;
            case UIStateEnum.SHOW:
                if (currentState == UIStateEnum.NOTINIT)
                {
                    Init();
                    Show();
                }
                else
                    Show();

                break;
            case UIStateEnum.HIDE:
                Hide();
                break; 
        }
    }
    protected virtual void Init()
    {
        if (InitAction != null)
            InitAction(gameObject);
    }

    protected virtual void Show()
    {
        UpdateShow();
        SetActive(true);
    }
    protected virtual void Hide()
    {
        SetActive(false);
    }
    /// <summary>
    /// update UI show
    /// </summary>
    protected virtual void UpdateShow()
    {

    }
    /// <summary>
    /// add init ui event
    /// </summary>
    /// <param name="action"></param>
    public override void AddInitListener(Func<UnityEngine.Object, bool> action)
    {
        InitAction = action;
    }

    /// <summary>
    ///  add show or hide event 
    /// </summary>
    /// <param name="action"></param>
    public override void AddInitListener(Func<bool, bool> action)
    {
        ObjectActiveActive = action;
    }

   /// <summary>
   /// get UI layer
   /// </summary>
   /// <returns></returns>
    public override UILayer Getlayer()
    {
        throw new NotImplementedException();
    }

    protected override IUIDataHandlerManager GetDataHandlerManager()
    {
        throw new NotImplementedException();
    }



  
    protected override void SetActive(bool isShow)
    {
        try
        {
            if (ObjectActiveActive == null)
                gameObject.SetActive(isShow);
            else
            {
                if (!ObjectActiveActive(isShow))
                    gameObject.SetActive(isShow);
            }
        }
        catch (System.Exception)
        {
            Debug.LogError(gameObject.name + "ObjectActiveActive has error");
            gameObject.SetActive(isShow);
        }
    }

    protected virtual T GetData<T>() where T:IData
    {
        if(dataHandler == null)
        {
            throw new Exception("This dataHandler is null. Please call the InitUI method in the Init metod to initialize the dataHandler");
        }
        return (T)dataHandler.GetData();
    }
  
}
