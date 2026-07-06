using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesMgr
{
    private static ResourcesMgr instance = new ResourcesMgr();
    public static ResourcesMgr Instance =>  instance;
    private ResourcesMgr(){}

    public void LoadRes<T>(string resName,UnityAction<T> callback) where T : Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(resName);
        request.completed += (a) =>
        {
            callback((a as ResourceRequest).asset as T);
        };
    }

    public IEnumerator LoadResToIEnumerator<T>(string resName, UnityAction<T> callback) where T : Object
    {
        ResourceRequest request =  Resources.LoadAsync<T>(resName);
        yield return request;
        callback(request.asset as T);
    }
}
