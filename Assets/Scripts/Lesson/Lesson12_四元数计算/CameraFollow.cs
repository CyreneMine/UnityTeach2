using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float zOffset;
    public float tiltDeg;
    public float cameraHeight;
    public float followSpeed =1;
    public float roundSpeed =1;
    private Vector3 startPos;
    private Vector3 nowTargetPos;
    private float followTime = 0;
    private float roundTime = 0;
    private Quaternion nowTargetRot;
    private Quaternion startRot;
    private void Start()
    {
        startPos = transform.position;
        nowTargetPos = target.position;
        nowTargetRot = target.rotation;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if (nowTargetPos != target.position ||nowTargetRot != target.rotation)
        {
            followTime = 0;
            nowTargetPos = target.position;
            startPos = transform.position;
            roundTime = 0;
            nowTargetRot = target.rotation;
            startRot = transform.rotation;
        }
        if (Input.GetAxis("Mouse ScrollWheel") <0)
        {
            zOffset = Mathf.Clamp(++zOffset, 3, 6);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            zOffset = Mathf.Clamp(--zOffset, 3, 6);
        }
        followTime += Time.deltaTime;
        roundTime += Time.deltaTime;
        
        /*Vector3 finalAt = target.position - zOffset * target.forward + xOffset * target.right + yOffset * target.up;
        transform.position = Vector3.Lerp(startPos, finalAt, followTime * followSpeed);
        Quaternion targetRotation = Quaternion.LookRotation
            (target.position+new Vector3(0,cameraHeight,0)
             - transform.position) * Quaternion.AngleAxis(tiltDeg, Vector3.right);
        transform.rotation = Quaternion.Slerp(startRot, targetRotation, roundTime*roundSpeed);*/
        
        //修正
        nowTargetPos = target.position +target.up * cameraHeight;
        Debug.DrawLine(transform.position, nowTargetPos, Color.red);
        Vector3 lookTargetDir = Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward;
        Vector3 lookTargetPos = nowTargetPos +lookTargetDir *zOffset;
        transform.position = Vector3.Lerp(startPos, lookTargetPos, followTime * followSpeed);
        transform.rotation = Quaternion.Slerp(startRot,Quaternion.LookRotation(-lookTargetDir),roundTime * roundSpeed);
        
        //理解过程
        // Vector3 nowTargetpos = target.position+target.up * cameraHeight;
        // Vector3 nowDir = Quaternion.AngleAxis(tiltDeg, target.right) * -target.forward;
        // Vector3 cameraPoint = nowTargetpos +nowDir * zOffset;
        // transform.position = Vector3.Lerp(startPos, cameraPoint, followTime * followSpeed);
        // transform.rotation = Quaternion.Slerp(startRot, Quaternion.LookRotation(-nowDir),roundTime * roundSpeed);
    }
}
