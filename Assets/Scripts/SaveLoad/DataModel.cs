using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataModel
{


    public const string HEALTH = "_health";
    public string lastSceneObj;
    public Dictionary<string, Vector3> dataDict = new Dictionary<string, Vector3>();
    public Dictionary<string, float> floatDataDict = new Dictionary<string, float>();


    /// <summary>
    /// 把指定的场景序列化成字符串
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public string getSceneDataString(SceneSO scene)
    {
        return JsonUtility.ToJson(scene);
    }

    /// <summary>
    /// 把保存的场景字符串反序列化为对象
    /// </summary>
    /// <returns></returns>
    public SceneSO GetSceneDataObj()
    {
        var sceneObj = ScriptableObject.CreateInstance<SceneSO>();
        JsonUtility.FromJsonOverwrite(lastSceneObj, sceneObj);
        return sceneObj;
    }
}
