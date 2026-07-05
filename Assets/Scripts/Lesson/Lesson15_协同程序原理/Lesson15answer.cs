using System.Collections;
using UnityEngine;

public class Lesson15answer : MonoBehaviour
{
    void Start()
    {
        CoroutineMgr.Instance.MyStartCoroutine(TimerEvent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimerEvent()
    {
        print("1");
        yield return 1;
        print("2");
        yield return 2;
        print("3");
        yield return 3;
        print("4");
    }
}
