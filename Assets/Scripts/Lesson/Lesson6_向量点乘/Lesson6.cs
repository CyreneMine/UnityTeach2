using UnityEngine;

public class Lesson6 : MonoBehaviour
{
    public float detectionAngle = 45;
    public float detectionDistance = 5;
    public GameObject target;
    void Update()
    {
        float dotResult = Vector3.Dot(transform.forward, (target.transform.position - transform.position).normalized);
        float angle = Mathf.Acos(dotResult)*Mathf.Rad2Deg;
        //一般用法 但是这里手写加深印象
        // angle = Vector3.Angle(transform.forward, target.transform.position - transform.position);
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (angle < detectionAngle/2 && distance < detectionDistance)
        {
            print("检测到敌人入侵！");
        }
    }
}
