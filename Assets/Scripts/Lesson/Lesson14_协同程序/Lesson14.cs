using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lesson14 : MonoBehaviour
{
    private int sec = 0;
    
    private void Start()
    {
        Coroutine timerSec = StartCoroutine(Timer());
        Coroutine randomCube = StartCoroutine(RandomCube(100000));
        
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print(++sec);            
        }
    }

    IEnumerator RandomCube(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position +=new Vector3(Random.Range(-100,101),Random.Range(-100,101),Random.Range(-100,101));
            if (i %1000 ==0)
                yield return null;
        }
    }
}

