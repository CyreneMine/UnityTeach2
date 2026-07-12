
using System.Collections.Generic;

public class FireData
{
    public List<FireInfo> fireInfoList = new List<FireInfo>();
}

public class FireInfo
{
    public int id;
    public int type;
    public int num;
    public float cd;
    public string ids;//规则: 1,10 代表1-10id的子弹中随机
    public float delay;
}
