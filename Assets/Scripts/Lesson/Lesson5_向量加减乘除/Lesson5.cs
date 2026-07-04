using System;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    public float zOffset;
    public float yOffset;
    public Transform target;
    private void LateUpdate()
    {
        //自己写的部分（错了）
        // transform.position = new Vector3(target.position.x, target.position.y+7, target.position.z-4);
        
        //教程写法
        transform.position = target.position -target.forward *zOffset +target.up * yOffset;
        transform.LookAt(target);
    }
}
