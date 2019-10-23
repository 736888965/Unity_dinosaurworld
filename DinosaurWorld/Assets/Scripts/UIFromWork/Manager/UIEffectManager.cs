using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
public class UIEffectManager : MonoBehaviour, IUIEffectManager
{
    private Dictionary<string, AUIEffect> effectDic = new Dictionary<string, AUIEffect>();


    public bool InitFun(string uiId, Object uiBase)
    {
        try
        {
            AUIEffect effect = ((GameObject)uiBase).GetComponent<AUIEffect>();
            if (effect != null)
            {
                effectDic[uiId] = effect;
                effect.AddEnterListener(() =>
                {
                    effect.uiShowState = UIShowState.Old;
                });
                effect.AddExitListener(() =>
                {
                    effect.uiShowState = UIShowState.New;
                    SetActive(uiId, false);
                });
                return true;
            }
            return false;
        }
        catch (System.Exception)
        {
            Debug.LogError("UIEffectManager.cs initfun has error");
            return false;
        }
    }

    public bool ActiveFun(string uiId,bool isActive)
    {
        if(effectDic.ContainsKey(uiId))
        {
            if (isActive)
            {
                SetActive(uiId, isActive);
                effectDic[uiId].Enter();
            }
            else
                effectDic[uiId].Exit();
            return true;
        }
        return false;
    }

    private void SetActive(string uiId,bool isActive)
    {
        effectDic[uiId].gameObject.SetActive(isActive);
    }
}

