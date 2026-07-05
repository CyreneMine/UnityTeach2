using UnityEngine;

public class Lesson11 : MonoBehaviour
{
    public Transform target;

    public Transform cubeA;
    void Update()
    {
        // Quaternion lookRotation = Quaternion.LookRotation(target.position - cubeA.position);
        // cubeA.rotation = lookRotation;
        cubeA.MyLookAt(target);
    }
}
