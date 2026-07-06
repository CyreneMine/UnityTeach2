using System;
using UnityEngine;

public class Lesson12 : MonoBehaviour
{
    public Transform airPlane;
    private GameObject bullet;
    public float sectorDeg = 60;

    private void Start()
    {
        bullet = Resources.Load<GameObject>("Bullet");
    }

    private void Update()
    {
        //单发
        if (Input.GetKeyDown(KeyCode.D))
        {
            Instantiate(bullet, airPlane.position, airPlane.rotation);
        }
        //双发
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Quaternion q = airPlane.rotation * Quaternion.AngleAxis(10, Vector3.up);
            // Instantiate(bullet, airPlane.position, q);
            // q = airPlane.rotation * Quaternion.AngleAxis(-10, Vector3.up);
            // Instantiate(bullet, airPlane.position, q);
            Instantiate(bullet, airPlane.position + Vector3.left, airPlane.rotation);
            Instantiate(bullet, airPlane.position + Vector3.right, airPlane.rotation);
        }
        //扇形
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (float i = -(sectorDeg/2); i <= sectorDeg/2; i+=10)
            {
                Quaternion q = airPlane.rotation * Quaternion.AngleAxis(i, Vector3.up);
                Instantiate(bullet, airPlane.position, q);
            }
        }
        //环形
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (float i = -(360/2); i <= 360/2; i+=10)
            {
                Quaternion q = airPlane.rotation * Quaternion.AngleAxis(i, Vector3.up);
                Instantiate(bullet, airPlane.position, q);
            }
        }
    }
}
