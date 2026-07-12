using System.Collections.Generic;

public class BulletData
{
    public List<BulletInfo> bulletInfoList = new List<BulletInfo>();
}

public class BulletInfo
{
    public int id;
    public int type;
    public float forwardSpeed;
    public float rightSpeed;
    public float roundSpeed;
    public string resName;
    public string deadEffRes;
    public float lifeTime;
}
