using System;
using System.Collections.Generic;

public class UIRoot : AUIRoot
{

    protected virtual void Start()
    {
        InitUISystem();
    }
    protected override void InitUISystem()
    {
        UIManager = new UIManager();
        if(LayerManager==null)
        {
            LayerManager = gameObject.AddComponent<UILayerManager>();
            LayerManager.Init();
        }
        if(UIEffectManager==null)
        {
            UIEffectManager = gameObject.AddComponent<UIEffectManager>();
            UIManager.AddUIActiveListener(UIEffectManager.ActiveFun);
            UIManager.AddUIInitListener(UIEffectManager.InitFun);
        }
    }
}

