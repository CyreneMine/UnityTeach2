using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YieldReturnTime
{
    //下一次要执行的时间
    public float time;
    public IEnumerator coroutine;
}
public class CoroutineMgr : MonoBehaviour
{
    private static CoroutineMgr instance;
    public static CoroutineMgr Instance => instance;
    private List<YieldReturnTime> coroutines = new List<YieldReturnTime>();
    private CoroutineMgr(){}
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        for (int i = coroutines.Count-1; i >=0; i--)
        {//后往前遍历以免中途删掉元素导致漏掉元素
            if (coroutines[i].time > Time.time)
                continue;
            if (coroutines[i].coroutine.MoveNext())
            {
                if (coroutines[i].coroutine.Current is int)
                {
                    coroutines[i].time = Time.time + (int)coroutines[i].coroutine.Current;
                }
            }
            else
            {
                coroutines.RemoveAt(i);
            }
        }
    }
    public void MyStartCoroutine(IEnumerator coroutine)
    {
        if (coroutine.MoveNext())
        {
            if (coroutine.Current is int)
            {
                YieldReturnTime yieldReturnTime = new YieldReturnTime();
                yieldReturnTime.coroutine = coroutine;
                yieldReturnTime.time = Time.time+(int)coroutine.Current;
                coroutines.Add(yieldReturnTime);
            }
            else
            {//不考虑 此题目只做时间相关
                
            }
        }
    }
}
