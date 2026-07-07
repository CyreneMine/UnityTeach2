using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneMgr
{
    private static SceneMgr instance = new SceneMgr();
    public static SceneMgr Instance => instance;
    private SceneMgr(){}

    public void LoadScene(string resName, UnityAction callback)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(resName);
        ao.completed += (a) =>
        {
            callback();
        };
    }
}
