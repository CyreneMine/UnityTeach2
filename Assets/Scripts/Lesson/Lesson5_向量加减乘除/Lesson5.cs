using System;
using UnityEngine;

public class Lesson5 : MonoBehaviour
{
    public float zOffset;
    public float yOffset;
    public Transform target;
    private Vector3 startPos;
    private float time;
    private Vector3 nowPos;
    private Quaternion nowRot;

    private void Start()
    {
        nowRot = target.rotation;
        startPos = transform.position;
        nowPos = target.position;
        time = 0;
    }

    private void LateUpdate()
    {
        //自己写的部分（错了）
        // transform.position = new Vector3(target.position.x, target.position.y+7, target.position.z-4);
        
        //教程写法
        // transform.position = target.position -target.forward *zOffset +target.up * yOffset;
        // transform.LookAt(target);
        
        //Lesson11 新写法
        // Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        // transform.rotation = lookRotation;
        if (nowPos != target.position || nowRot != target.rotation)
        {
            nowPos = target.position;
            time = 0;
            startPos = transform.position;
            nowRot = target.rotation;
        }
        time += Time.deltaTime;
        // transform.position = target.position -target.forward *zOffset +target.up * yOffset;
        transform.position = Vector3.Lerp(startPos, target.position - target.forward * zOffset + target.up * yOffset, time);
        transform.rotation =Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), time*0.1f) ;
        
    }
}
