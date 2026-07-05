using UnityEngine;

public static class Tools
{
    public static void MyLookAt(this Transform obj, Transform target)
    {
        Vector3 targetDic =  target.position - obj.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDic);
        obj.rotation = lookRotation;
    }

}
