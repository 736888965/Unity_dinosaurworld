using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 代码思想
/// 1将所有地标存到字典中，这样可以利用名字取地标显示和隐藏
/// 2利用switch来控制相应的地标显示
/// </summary>
public class DiBiaoShow : MonoBehaviour
{
    /// <summary>
    /// 地标字典，方便Key查询
    /// </summary>
    public Dictionary<string, Transform> DiBiaoDIC;
    /// <summary>
    /// 临时地标List，将各个地址先存到list中
    /// </summary>
    public List<Transform> DiBiaoList;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        //设置所有地标隐藏
        foreach (Transform item in DiBiaoList)
        {
            item.gameObject.SetActive(false);
        }


    }



    // Use this for initialization
    void Awake()
    {
        DiBiaoDIC = new Dictionary<string, Transform>();
        //将所有地标放到list中
        foreach (Transform item in transform)
        {
            DiBiaoList.Add(item);
        }
        //for循环，将list中的地标放到字典中
        foreach (Transform item in DiBiaoList)
        {
            string tempName = item.name;
            print(tempName);

            DiBiaoDIC.Add(tempName, item);
        }

        Initialize();

        //ShowDiBiao("Mixosaurus");
    }


    /// <summary>
    /// 以下是将所有地标名字变成常量，方便输入
    /// 地标名字：ZhongGuoBiao 中国、YaZhou 亚洲、YiSeLie 以色列、ELuoSi 俄罗斯、JiaNaDa 加拿大、BeiMeiZhou 北美洲、MeiGuo 美国、YinNi 印尼
    /// GeLunBiYa 哥伦比亚、BaXi 巴西、DiWen 帝汶、DeGuo 德国、YiDaLi 意大利、OuZhou 欧洲、FaGuo 法国、AoZhou 澳洲、MengGu 蒙古、XiBanYa 西班牙、
    /// MeiGuoALSJ 美国拉斯维加斯、AGenTing 阿根廷、
    /// </summary>
    public const string ZhongGuo = "ZhongGuo";
    public const string YaZhou = "YaZhou";
    public const string YiSeLie = "YiSeLie";
    public const string ELuoSi = "ELuoSi";
    public const string JiaNaDa = "JiaNaDa";
    public const string BeiMeiZhou = "BeiMeiZhou";
    public const string MeiGuo = "MeiGuo";
    public const string YinNi = "YinNi";
    public const string GeLunBiYa = "GeLunBiYa";
    public const string BaXi = "BaXi";
    public const string DiWen = "DiWen";
    public const string DeGuo = "DeGuo";
    public const string YiDaLi = "YiDaLi";
    public const string OuZhou = "OuZhou";
    public const string FaGuo = "FaGuo";
    public const string AoZhou = "AoZhou";
    public const string MengGu = "MengGu";
    public const string XiBanYa = "XiBanYa";
    public const string MeiGuoALSJ = "MeiGuoALSJ";
    public const string AGenTing = "AGenTing";


    /// <summary>
    /// 传入一个名字，然后根据名字来控制字典里的所有东西的隐藏和显示
    /// 地标名字：ZhongGuoBiao 中国、YaZhou 亚洲、YiSeLie 以色列、ELuoSi 俄罗斯、JiaNaDa 加拿大、BeiMeiZhou 北美洲、MeiGuo 美国、YinNi 印尼
    /// GeLunBiYa 哥伦比亚、BaXi 巴西、DiWen 帝汶、DeGuo 德国、YiDaLi 意大利、OuZhou 欧洲、FaGuo 法国、AoZhou 澳洲、MengGu 蒙古、XiBanYa 西班牙、
    /// MeiGuoALSJ 美国拉斯维加斯、AGenTing 阿根廷、
    /// </summary>
    /// <param name="name"></param>

    public void ShowDiBiao(string name)
    {
        //先初始化，让所有的子地标隐藏
        Initialize();

        switch (name)
        {
            case "Nothosaurus":
                DICShow(DeGuo,XiBanYa,YiSeLie, ZhongGuo);
                break;
            case "Mixosaurus":
                DICShow(ZhongGuo,DiWen,YinNi,YiDaLi,JiaNaDa,MeiGuoALSJ,MeiGuo);
                break;
            case "Shastasaurus":
                DICShow(MeiGuo,JiaNaDa,ZhongGuo);
                break;
            case "Keichousaurus":
                DICShow(ZhongGuo);
                break;
            case "Guanlong":
                DICShow(ZhongGuo);
                break;
            case "Yinlong":
                DICShow(ZhongGuo);
                break;
            case "Limusaurus":
                DICShow(ZhongGuo);
                break;
            case "Caudipteryx":
                DICShow(ZhongGuo);
                break;
            case "Microraptor":
                DICShow(ZhongGuo);
                break;
            case "Liaoningosaurus":
                DICShow(ZhongGuo);
                break;
            case "Psittacosaurus":
                DICShow(ZhongGuo);
                break;
            case "Huaxiapterus":
                DICShow(ZhongGuo);
                break;
            case "Zhangheotherium":
                DICShow(ZhongGuo);
                break;
            case "Zhejiangosaurus":
                DICShow(ZhongGuo);
                break;
            case "Yueosaurus":
                DICShow(ZhongGuo);
                break;
            case "Therizinosauridae":
                DICShow(ZhongGuo);
                break;
            case "Jinyunpelta":
                DICShow(ZhongGuo);
                break;
            case "Tupuxuara":
                DICShow(BaXi);
                break;
            case "Pteranodon":
                DICShow(BeiMeiZhou);
                break;
            case "Plioplatecarpus":
                DICShow(OuZhou);
                break;
            case "Elasmosaurus":
                DICShow(MeiGuo);
                break;
            case "Herrerasaurus":
                DICShow(AGenTing);
                break;
            case "Toujiangosaurus":
                DICShow(ZhongGuo);
                break;
            case "Triceratops":
                DICShow(MeiGuo);
                break;
            case "Tyrannosaurus":
                DICShow(MeiGuo,JiaNaDa);
                break;
            case "Hadrosaurus":
                DICShow(MeiGuo);
                break;
            case "Euoplocephalus":
                DICShow(BeiMeiZhou);
                break;
            case "Eoraptor":
                DICShow(AGenTing);
                break;
            case "Eodromaeus":
                DICShow(AGenTing);
                break;
            case "Coelophysis":
                DICShow(BeiMeiZhou);
                break;
            case "Anchiornis":
                DICShow(ZhongGuo);
                break;
            case "Compsognathus":
                DICShow(DeGuo,FaGuo);
                break;
            case "Yunnanosaurus":
                DICShow(ZhongGuo);
                break;
            case "Stegosaurus":
                DICShow(BeiMeiZhou);
                break;
            case "Allosaurus":
                DICShow(BeiMeiZhou);
                break;
            case "Velociraptor":
                DICShow(MengGu);
                break;
            case "Protoceratops":
                DICShow(MengGu);
                break;
            case "Oviraptor":
                DICShow(MengGu,ZhongGuo);
                break;
            case "Archaeornithomi":
                DICShow(YaZhou);
                break;
            case "Ornithomimus":
                DICShow(BeiMeiZhou);
                break;
            case "Albertosaurus":
                DICShow(JiaNaDa);
                break;
            case "Maiasaura":
                DICShow(MeiGuo);
                break;
            case "Ophthalmosaurus":
                DICShow(BeiMeiZhou,OuZhou,AGenTing);
                break;
            case "Platypterygius":
                DICShow(OuZhou,ELuoSi,MeiGuo,GeLunBiYa,AoZhou);
                break;
            case "Zhejiangopterus":
                DICShow(ZhongGuo);
                break;
            case "Darwinopterus":
                DICShow(ZhongGuo);
                break;
            case "Dinocephalosaurus":
                DICShow(ZhongGuo);
                break;
            case "Sinosauropteryx":
                DICShow(ZhongGuo);
                break;
            case "Archaeopteryx":
                DICShow(DeGuo);
                break;
            case "Prestosuchus":
                DICShow(BaXi);
                break;
            case "Yandangornis":
                DICShow(ZhongGuo);
                break;

        }


    }

   ////////////////////////////////////////以下代码是取字典里的物体显示的，方法多态，最多传7个值////////////////////////////////////////
   
    /// <summary>
    /// 根据传入的名字，显示字典里存的子地标
    /// </summary>
    /// <param name="name"></param>

    public void DICShow(string name )
    {
        print("！！！！！！！！！" + name);
        DiBiaoDIC[name].gameObject.SetActive(true);
        
    }
    /// <summary>
    /// 根据传入的名字，显示字典里存的子地标
    /// </summary>
    /// <param name="name"></param>

    public void DICShow(string name1, string name2)
    {
        DiBiaoDIC[name1].gameObject.SetActive(true);
        DiBiaoDIC[name2].gameObject.SetActive(true);
    }
    /// <summary>
    /// 根据传入的名字，显示字典里存的子地标
    /// </summary>
    /// <param name="name"></param>

    public void DICShow(string name1, string name2, string name3)
    {
        DiBiaoDIC[name1].gameObject.SetActive(true);
        DiBiaoDIC[name2].gameObject.SetActive(true);
        DiBiaoDIC[name3].gameObject.SetActive(true);
    }
    /// <summary>
    /// 根据传入的名字，显示字典里存的子地标
    /// </summary>
    /// <param name="name"></param>

    public void DICShow(string name1, string name2, string name3, string name4)
    {
        Debug.LogError(name1 + "     " + name2 + "   " + name3 + "           " + name4);
        DiBiaoDIC[name1].gameObject.SetActive(true);
        DiBiaoDIC[name2].gameObject.SetActive(true);
        DiBiaoDIC[name3].gameObject.SetActive(true);
        DiBiaoDIC[name4].gameObject.SetActive(true);

    }
    /// <summary>
    /// 根据传入的名字，显示字典里存的子地标
    /// </summary>
    /// <param name="name"></param>

    public void DICShow(string name1, string name2, string name3, string name4, string name5)
    {
        DiBiaoDIC[name1].gameObject.SetActive(true);
        DiBiaoDIC[name2].gameObject.SetActive(true);
        DiBiaoDIC[name3].gameObject.SetActive(true);
        DiBiaoDIC[name4].gameObject.SetActive(true);
        DiBiaoDIC[name5].gameObject.SetActive(true);

    }
    /// <summary>
    /// 根据传入的名字，显示字典里存的子地标
    /// </summary>
    /// <param name="name"></param>

    public void DICShow(string name1, string name2, string name3, string name4, string name5, string name6)
    {
        DiBiaoDIC[name1].gameObject.SetActive(true);
        DiBiaoDIC[name2].gameObject.SetActive(true);
        DiBiaoDIC[name3].gameObject.SetActive(true);
        DiBiaoDIC[name4].gameObject.SetActive(true);
        DiBiaoDIC[name5].gameObject.SetActive(true);
        DiBiaoDIC[name6].gameObject.SetActive(true);

    }
    public void DICShow(string name1, string name2, string name3, string name4, string name5, string name6, string name7)
    {
        DiBiaoDIC[name1].gameObject.SetActive(true);
        DiBiaoDIC[name2].gameObject.SetActive(true);
        DiBiaoDIC[name3].gameObject.SetActive(true);
        DiBiaoDIC[name4].gameObject.SetActive(true);
        DiBiaoDIC[name5].gameObject.SetActive(true);
        DiBiaoDIC[name6].gameObject.SetActive(true);
        DiBiaoDIC[name7].gameObject.SetActive(true);
    }
}

