using UnityEngine;

public class Lesson7 : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        float dot = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
        Vector3 cross = Vector3.Cross(transform.forward, (target.position - transform.position).normalized);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        float distance = Vector3.Distance(transform.position, target.position);
        if (dot <= 0)
        {
            print(cross.y > 0?"右后方":"左后方");
        }else if (dot > 0)
        {
            print(cross.y > 0?"右前方":"左前方");
        }

        if (cross.y > 0)
        {
            if (angle<=30 && distance <=5)
            {
                print("检测到敌人入侵！");
            }
        }
        else
        {
            if (angle<=20 && distance <=5)
            {
                print("检测到敌人入侵！");
            }
        }
    }
}
