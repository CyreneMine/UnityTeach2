using System.Collections.Generic;
using UnityEngine;

public class RoleData
{
    public List<RoleInfo> list = new List<RoleInfo>();
}

public class RoleInfo
{
    public int hp;
    public int speed;
    public int volume;
    public string resName;
    public float scale;
}