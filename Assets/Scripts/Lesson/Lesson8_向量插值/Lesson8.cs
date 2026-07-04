using System;
using UnityEngine;

public class Lesson8 : MonoBehaviour
{
    public Transform target;
    public Transform sun;
    private float time = 0;
    private Vector3 nowTargetPos;
    private Vector3 startPos;
    public float zOffset;
    public float yOffset;
    private Vector3 finalPos;
    private float sunTime;
    private void Start()
    {
        nowTargetPos = target.position;
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        if (nowTargetPos != target.position)
        {
            nowTargetPos = target.position;
            time = 0;
            startPos = transform.position;
        }
        time += Time.deltaTime;
        finalPos = nowTargetPos -target.forward * zOffset + target.up * yOffset;
        transform.position = Vector3.Lerp(startPos, finalPos, time);
        transform.LookAt(target);
        sunTime += Time.deltaTime;
        sun.position = Vector3.Slerp(Vector3.right*10,Vector3.left*10 +Vector3.up,sunTime*0.3f);
    }
}
