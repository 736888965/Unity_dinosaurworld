using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseAdapt : MonoBehaviour
{
    public HorizontalLayoutGroup m_uiTopBg;
    void Awake()
    {
        // #if UNITY_IOS || UNITY_IPHONE
        if (IsIphoneXOrLate())
        {
            if (m_uiTopBg != null)
            {
                m_uiTopBg.padding.top += 40;
            }
        }
    }

    public bool IsIphoneXOrLate()
    {

        bool IsIphoneXDevice = false;
#if UNITY_IOS || UNITY_IPHONE

        string modelStr = SystemInfo.deviceModel;
        // iPhoneX:"iPhone10,3","iPhone10,6"  iPhoneXR:"iPhone11,8"  iPhoneXS:"iPhone11,2"  iPhoneXS Max:"iPhone11,6"
        IsIphoneXDevice = modelStr.Equals("iPhone10,3") || modelStr.Equals("iPhone10,6") || modelStr.Equals("iPhone11,8") || modelStr.Equals("iPhone11,2") || modelStr.Equals("iPhone11,6");

#elif UNITY_ANDROID

#endif
        return IsIphoneXDevice;
    }


}