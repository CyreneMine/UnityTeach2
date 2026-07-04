using UnityEngine;

public class Lesson2 : MonoBehaviour
{
    public float speed = 1f;
    private float timer = 0;
    public float changeSpeed = 0.1f;
    public float changeSize = 2f;
    void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        timer += Time.deltaTime* changeSpeed;
        transform.Translate(Vector3.right * (Time.deltaTime * changeSize * Mathf.Sin(timer) ));
        //自己写法留档
        // transform.position = new Vector3(
        //     transform.position.x+Mathf.Sin(nowSinDeg * Mathf.Deg2Rad)*Time.deltaTime,
        //     transform.position.y,
        //     transform.position.z);
    }
}
