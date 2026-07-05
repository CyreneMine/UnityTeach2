using System;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    private void Start()
    {
        transform.rotation *= Quaternion.AngleAxis(90, Vector3.right);
    }

    private float time = 0;
    void Update()
    {
        if (time >3)
        {
            Destroy(gameObject);
        }
        time += Time.deltaTime;
        transform.Translate(Vector3.up*Time.deltaTime*10);
    }
}
