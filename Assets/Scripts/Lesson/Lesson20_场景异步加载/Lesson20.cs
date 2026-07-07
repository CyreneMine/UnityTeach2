using System;
using UnityEngine;

public class Lesson20 : MonoBehaviour
{
    private void Start()
    {
        SceneMgr.Instance.LoadScene("Lesson20_2",(() =>
        {
            print("加载完成，开始跳转");
        }));
    }
}
