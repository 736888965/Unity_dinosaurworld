using System;
using System.Collections.Generic;
using UnityEngine;

public enum EUiId
{
    MAIN_UI,
    VIEW_ONE,
    VIEW_TWO,
    SIDE_VIEW,
    DOALOG,
}

public delegate void OnTrackingFound(Transform model, Transform parent);

