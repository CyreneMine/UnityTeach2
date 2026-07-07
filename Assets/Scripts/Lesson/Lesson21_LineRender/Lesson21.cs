using System;
using Unity.VisualScripting;
using UnityEngine;

public class Lesson21 : MonoBehaviour
{
    public Vector3 center;
    public float radius;
    private LineRenderer lineRenderer;
    private LineRenderer line2;
    private bool flag = false;
    private void Start()
    {
        // DrawSphere(center, radius);
        line2 = this.AddComponent<LineRenderer>();
        line2.loop = false;
        line2.positionCount = 0;
    }

    private void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
           flag = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            flag = false;
        }
        if (flag)
        {
            print(Input.mousePosition);
            line2.positionCount += 1;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            line2.SetPosition(line2.positionCount-1,pos);
        }
    }

    public void DrawSphere(Vector3 center, float radius)
    {
        if (GetComponent<LineRenderer>() == null)
        {
            lineRenderer = this.AddComponent<LineRenderer>();
        }
        lineRenderer.loop = true;
        lineRenderer.positionCount = 360;
        for (int i = 0; i < 360; i++)
        {
            lineRenderer.SetPosition(i,center+Quaternion.AngleAxis(i,Vector3.up)*Vector3.forward*radius);
        }
    }
}
