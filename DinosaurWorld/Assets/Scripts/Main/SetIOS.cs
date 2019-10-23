using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetIOS : UIBaseAdapt
{

    public LayoutElement toplayout;
    public LayoutElement butlayout;
    public HorizontalLayoutGroup horizontalLayoutGroup;
    private void Start()
    {
        if (toplayout != null && IsIphoneXOrLate())
        {
            toplayout.minHeight = 200;
        }
        if (butlayout != null && IsIphoneXOrLate())
        {
            butlayout.minHeight = 300;
        }
        if (horizontalLayoutGroup != null && IsIphoneXOrLate())
        {
            horizontalLayoutGroup.padding.bottom = 125;
        }
    }
}
