using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebRequestAPI  {


    /// <summary>
    /// 注册
    /// </summary>
    public const string Register = "home/User/register";
    /// <summary>
    /// 登录
    /// </summary>
    public const string Login = "home/User/login";
    /// <summary>
    /// 登出
    /// </summary>
    public const string LoginOut = "home/User/loginOut";
    /// <summary>
    /// 用户扫描恐龙接口
    /// </summary>
    public const string Scanning = "home/User/scanning";
    /// <summary>
    /// 用户扫描恐龙退出
    /// </summary>
    public const string ScanningOut = "home/User/scanningOut";
    /// <summary>
    /// 点赞
    /// </summary>
    public const string Fabulous = "home/User/fabulous";

    /// <summary>
    /// 取消点赞
    /// </summary>
    public const string unFabulous = "home/User/unFabulous";
}

public class WebParameter
{
    public const string userName = "userName";

    public const string passWorld = "passWord";

    public const string timeStamp = "timeStamp";

    public const string loginLogId = "loginLogId";

    public const string userId = "userId";

    public const string dID = "dID";

    public const string scanningId = "scanningId";

    public const string fabulousId= "fabulousId";
}

public class WebType
{
    /// <summary>
    /// 点赞
    /// </summary>
    public const string fabulous = "fabulous";
    /// <summary>
    /// 取消点赞
    /// </summary>
    public const string unFabulous = "unFabulous";
    /// <summary>
    /// 扫描
    /// </summary>
    public const string scanning = "scanning";

    /// <summary>
    /// 退出扫描
    /// </summary>
    public const string scanningOut = "scanningOut";

}

