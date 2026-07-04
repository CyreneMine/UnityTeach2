using UnityEngine;

public class Lesson1 : MonoBehaviour
{
    public GameObject cube;
    public GameObject sphere;
    public float time;
    public Vector3 nowPosition;
    public Vector3 startPosition;
    // Update is called once per frame
    void Update()
    {
        //曲线移动
        // Vector3 newCubePos = cube.transform.position;
        // newCubePos.z = Mathf.Lerp(newCubePos.z, sphere.transform.position.z, Time.deltaTime);
        // newCubePos.x = Mathf.Lerp(newCubePos.x, sphere.transform.position.x, Time.deltaTime);
        // newCubePos.z = Mathf.Lerp(newCubePos.z, sphere.transform.position.z, Time.deltaTime);
        // cube.transform.position = newCubePos;
        //匀速移动
        if (nowPosition!=sphere.transform.position)
        {
            time = 0;
            nowPosition = sphere.transform.position;
            startPosition = cube.transform.position;
        }
        Vector3 pos;
        time += Time.deltaTime;
        pos.x = Mathf.Lerp(startPosition.x, nowPosition.x, time);
        pos.y = Mathf.Lerp(startPosition.y, nowPosition.y, time);
        pos.z = Mathf.Lerp(startPosition.z, nowPosition.z, time);
        cube.transform.position = pos;
    }
}
