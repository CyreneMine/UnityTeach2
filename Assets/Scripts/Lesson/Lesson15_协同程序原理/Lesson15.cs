using System;
using System.Collections;
using UnityEngine;

public class Lesson15 : MonoBehaviour
{
    public int timeCount = 0;
    public float sec = 0;
    private IEnumerator timer;
    private bool isGet = false;
    private void Start()
    {
        timer = Timer();
    }

    private void Update()
    {
        sec += Time.deltaTime;
        if (sec >= 1)
        {
            isGet = true;
            sec = 0;
        }
        timer.MoveNext();
    }

    IEnumerator Timer()
    {
        while (true)
        {
            if (isGet)
            {
                print(timeCount++);
                yield return isGet = false;
            }
            yield return null;
        }
    }
}
