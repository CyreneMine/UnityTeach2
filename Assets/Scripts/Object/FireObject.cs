using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FireObjectType
{
    TopLeft,
    Top,
    TopRight,
    Left,
    Right,
    BottomLeft,
    Bottom,
    BottomRight
}
public class FireObject : MonoBehaviour
{
    public FireObjectType type;
    private Vector3 screenPos;
    private Vector3 initDir;
    private Vector3 nowDir;
    private FireInfo fireInfo;
    private int nowNum;
    private float nowCd;
    private float nowDelay;
    private BulletInfo nowBullet;
    private float changeAngle;
    void Update()
    {
        UpdatePos();
        ResetFireInfo();
        UpdateFire();
    }

    private GameObject bullet;
    private BulletObject bulletObj;
    private void UpdateFire()
    {
        if (nowCd ==0 && nowNum == 0)
            return;
        nowCd-=Time.deltaTime;
        if (nowCd > 0)
            return;
        switch (fireInfo.type)
        {
            case 1:
                bullet = Instantiate(Resources.Load<GameObject>(nowBullet.resName));
                bulletObj = bullet.gameObject.AddComponent<BulletObject>();
                bulletObj.InitInfo(nowBullet);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position);
                --nowNum;
                nowCd = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case 2:
                if (nowCd == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(nowBullet.resName));
                        bulletObj = bullet.gameObject.AddComponent<BulletObject>();
                        bulletObj.InitInfo(nowBullet);
                        bullet.transform.position = transform.position;
                        nowDir = Quaternion.AngleAxis(changeAngle*i, Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }

                    nowCd = nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBullet.resName));
                    bulletObj = bullet.gameObject.AddComponent<BulletObject>();
                    bulletObj.InitInfo(nowBullet);
                    bullet.transform.position = transform.position;
                    nowDir = Quaternion.AngleAxis(changeAngle*(fireInfo.num-nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    --nowNum;
                    nowCd = nowNum == 0 ? 0 : fireInfo.cd;
                }
                break;
        }
        
        
    }

    public void UpdatePos()
    {
        screenPos.z = Camera.main.WorldToScreenPoint(PlayerObject.Instance.transform.position).z;
        switch (type)
        {
            case FireObjectType.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case FireObjectType.Top:
                screenPos.x = Screen.width/2;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case FireObjectType.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                initDir = Vector3.left;
                break;
            case FireObjectType.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height/2;
                initDir = Vector3.right;
                break;
            case FireObjectType.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height/2;
                initDir = Vector3.left;
                break;
            case FireObjectType.BottomLeft:
                initDir = Vector3.right;
                screenPos.x = 0;
                screenPos.y = 0;
                break;
            case FireObjectType.Bottom:
                initDir = Vector3.right;
                screenPos.x = Screen.width/2;
                screenPos.y = 0;
                break;
            case FireObjectType.BottomRight:
                initDir = Vector3.left;
                screenPos.x = Screen.width;
                screenPos.y = 0;
                break;
        }
        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }

    public void ResetFireInfo()
    {
        if (nowCd != 0 && nowNum !=0)
            return;
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay >=0)
                return;
        }
        List<FireInfo> infos = GameDataMgr.Instance.fireData.fireInfoList;
        fireInfo = infos[Random.Range(0, infos.Count)];
        nowNum = fireInfo.num;
        nowCd = fireInfo.cd;
        nowDelay = fireInfo.delay;
        string[] strs = fireInfo.ids.Split(',');
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        nowBullet = GameDataMgr.Instance.bulletData.bulletInfoList[Random.Range(beginID, endID+1)-1];
        if (fireInfo.type == 2)
        {
            switch (type)
            {
                case FireObjectType.TopLeft:  
                case FireObjectType.TopRight:
                case FireObjectType.BottomRight:
                case FireObjectType.BottomLeft:
                    changeAngle = 90 / (nowNum+1);
                    break;
                case FireObjectType.Top:
                case FireObjectType.Left:
                case FireObjectType.Right:
                case FireObjectType.Bottom:
                    changeAngle = 180 / (nowNum+1);
                    break;
               
            }
        }
    }
}
