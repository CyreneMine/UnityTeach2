using System;
using UnityEngine;

public class Lesson13 : MonoBehaviour
{
    private int count = 0;
    private void Start()
    {
        InvokeRepeating("DelayTime", 0, 1);
        Destroy(gameObject, 2f);
        Invoke("DelayDestroy",2f);
    }

    public void DelayTime()
    {
        print(count++);
    }
    private void DelayDestroy()
    {
        Destroy(gameObject);
    }
}
