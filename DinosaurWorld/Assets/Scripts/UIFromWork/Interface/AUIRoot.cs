using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUIRoot : MonoBehaviour
{
    public static IUIDataHandlerManager DataHandlerManager { get; protected set;}

    public static IUIManager UIManager { get; protected set; }

    public static IUILayerManager LayerManager { get; protected set; }

    public static IUIEffectManager UIEffectManager { get; protected set; }

    protected abstract void InitUISystem();
    }

