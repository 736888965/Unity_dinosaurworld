using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InfoCode
{
    public int id;
    public string english;
    public string name;
    public string info;

}


[System.Serializable]
public class InfoList
{
    public List<InfoCode> infoList;
}


[System.Serializable]
public class IdName
{
    public int id;
    public string name;
    public int camera;
    public int earth;
}


[System.Serializable]
public class IdNameList
{
    public List<IdName> infoList;
}


