using System.IO;
using LitJson;
using UnityEngine;

public enum JsonType
{
    JsonUtility,
    LitJson,
}
public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;
    private JsonMgr() {}

    public void SaveData(object data,string fileName, JsonType jsonType = JsonType.LitJson)
    {
        string path = Application.persistentDataPath +$"/{fileName}.json";
        string jsonStr = "";
        switch (jsonType)
        {
            case JsonType.LitJson:
                jsonStr = LitJson.JsonMapper.ToJson(data);
                break;
            case JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
        }
        File.WriteAllText(path,jsonStr);
    }

    public T LoadData<T>(string fileName, JsonType jsonType = JsonType.LitJson) where T : new()
    {
        string path = Application.streamingAssetsPath + $"/{fileName}.json";
        if (!File.Exists(path))
            path = Application.persistentDataPath +$"/{fileName}.json";
        if (!File.Exists(path))
            return new T();
        T data = default(T);
        switch (jsonType)
        {
            case JsonType.LitJson:
                data = LitJson.JsonMapper.ToObject<T>(File.ReadAllText(path));
                break;
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(File.ReadAllText(path));
                break;
        }
        return data;
    }
}
